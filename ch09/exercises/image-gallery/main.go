package main

import (
	"bytes"
	"encoding/json"
	"html/template"
	"io/ioutil"	
	"math/rand"
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
		Help: "Image Gallery - in-flight requests",
	})
	requestCounter := prometheus.NewCounterVec(
		prometheus.CounterOpts{
			Name: "image_gallery_requests_total",
			Help: "Image Gallery - total requests",
		},
		[]string{"code", "method"},
	)

	prometheus.MustRegister(inFlightGauge, requestCounter)

	rand.Seed(time.Now().UnixNano())

	indexHandler := http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		//random failures:
		if (rand.Intn(10) > 8) {
			w.WriteHeader(http.StatusInternalServerError)
			w.Write([]byte("Failed"))
		} else {
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
		}
	})

	wrappedIndexHandler := promhttp.InstrumentHandlerInFlight(inFlightGauge,
							promhttp.InstrumentHandlerCounter(requestCounter, indexHandler))
	
	http.Handle("/", wrappedIndexHandler)
	http.Handle("/metrics", promhttp.Handler())
	
	http.ListenAndServe(":80", nil)
}