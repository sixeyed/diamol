package main

import (
	"bytes"
	"encoding/json"
	"html/template"
	"io/ioutil"	
	"net/http"
	"os"
	"time"
	"github.com/prometheus/client_golang/prometheus/promhttp"
	"github.com/prometheus/client_golang/prometheus"
)

type Image struct {
	Url       string `json:"url"`
	Caption   string `json:"caption"`
	Copyright string `json:"copyright"`
}

type AccessLog struct {
	ClientIP  string `json:"clientIp"`
}

func main() {
	tmpl := template.Must(template.ParseFiles("index.html"))
	imageApiUrl :=  os.Getenv("IMAGE_API_URL")
	logApiUrl := os.Getenv("ACCESS_API_URL")

	//re-use HTTP client with minimal keep-alive
	tr := &http.Transport{
		MaxIdleConns: 1,
	    IdleConnTimeout: 1 * time.Second,
	}
	client := &http.Client{Transport: tr}

	//create Prometheus metrics
	inFlightGauge := prometheus.NewGauge(prometheus.GaugeOpts{
		Name: "image_gallery_in_flight_requests",
		Help: "Image Galler - in-flight requests",
	})

	counter := prometheus.NewCounterVec(
		prometheus.CounterOpts{
			Name: "image_gallery_requests_total",
			Help: "Image Gallery - total requests",
		},
		[]string{"code", "method"},
	)
	duration := prometheus.NewHistogramVec(
		prometheus.HistogramOpts{
			Name:    "image_gallery_request_duration_seconds",
			Help:    "Image Gallery - request durations",
			Buckets: []float64{.25, .5, 1, 2.5, 5, 10},
		},
		[]string{"handler", "method"},
	)
	responseSize := prometheus.NewHistogramVec(
		prometheus.HistogramOpts{
			Name:    "image_gallery_response_size_bytes",
			Help:    "Image Gallery - response sizes",
			Buckets: []float64{200, 500, 900, 1500},
		},
		[]string{},
	)

	prometheus.MustRegister(inFlightGauge, counter, duration, responseSize)

	indexHandler := http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		response,_ := client.Get(imageApiUrl)
		defer response.Body.Close()
		data,_ := ioutil.ReadAll(response.Body)
		image := Image{}
		json.Unmarshal([]byte(data), &image)	
		tmpl.Execute(w, image)

		log := AccessLog{
			ClientIP: r.RemoteAddr,
		}
		jsonLog,_ := json.Marshal(log)
		response,_ = client.Post(logApiUrl, "application/json", bytes.NewBuffer(jsonLog))
		defer response.Body.Close()
	})

	wrappedIndexHandler := promhttp.InstrumentHandlerInFlight(inFlightGauge,
		promhttp.InstrumentHandlerDuration(duration.MustCurryWith(prometheus.Labels{"handler": "index"}),
			promhttp.InstrumentHandlerCounter(counter,
				promhttp.InstrumentHandlerResponseSize(responseSize, indexHandler),
			),
		),
	)
	
	http.Handle("/", wrappedIndexHandler)
	http.Handle("/metrics", promhttp.Handler())
	
	http.ListenAndServe(":80", nil)
}