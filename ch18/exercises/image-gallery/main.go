package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"html/template"
	"io/ioutil"	
	"math/rand"
	"net/http"
	"time"
	"github.com/prometheus/client_golang/prometheus/promhttp"
	"github.com/prometheus/client_golang/prometheus"
	"github.com/spf13/viper"
)

type Configuration struct {
	Environment string
    Apis map[string]Api `mapstructure:"apis"`
}

type Api struct {    
    Url string
}

type Image struct {
	Url       string `json:"url"`
	Caption   string `json:"caption"`
	Copyright string `json:"copyright"`
}

type AccessLog struct {
	ClientIP  string `json:"clientIp"`
}

func getConfig() Configuration {
	viper.SetEnvPrefix("IG")
	viper.AutomaticEnv()
	viper.SetConfigName("config")
	viper.AddConfigPath("/secrets")
	viper.AddConfigPath("/config")
	viper.AddConfigPath(".")
	
	config := Configuration{}
	_ = viper.ReadInConfig()	
	_ = viper.Unmarshal(&config)

	return config
}

func main() {
	config := getConfig()
	imageApiUrl :=  config.Apis["image"].Url
	logApiUrl := config.Apis["access"].Url
	//imageApiUrl :=  viper.GetString("apis.image.url")
	//logApiUrl := viper.GetString("apis.access.url")
	fmt.Printf("Environment: %v\n", config.Environment) 
	fmt.Printf("Image API: %v\n", imageApiUrl) 
	fmt.Printf("Access API: %v\n", logApiUrl) 

	tmpl := template.Must(template.ParseFiles("index.html"))
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