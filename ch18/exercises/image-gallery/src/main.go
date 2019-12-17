package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"html/template"
	"io/ioutil"	
	"net/http"
	"time"
	"github.com/prometheus/client_golang/prometheus/promhttp"
	"github.com/prometheus/client_golang/prometheus"
	"github.com/spf13/viper"
)

type Configuration struct {
	Release string
	Environment string
	Metrics Metrics
    Apis map[string]Api `mapstructure:"apis"`
}

type Metrics struct {    
    Enabled bool
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
	
	viper.SetConfigFile("./config.toml")
	viper.MergeInConfig()
	viper.SetConfigFile("./config-override/config.toml")
	viper.MergeInConfig()

	config := Configuration{}	
	viper.Unmarshal(&config)
	return config
}

func ConfigHandler(w http.ResponseWriter, req *http.Request) {
	config := getConfig()
	json.NewEncoder(w).Encode(config)
}

func main() {
	config := getConfig()
	imageApiUrl :=  config.Apis["image"].Url
	logApiUrl := config.Apis["access"].Url
	fmt.Printf("Environment: %v, metrics enabled: %v\n", config.Environment, config.Metrics.Enabled) 

	tmpl := template.Must(template.ParseFiles("index.html"))
	//re-use HTTP client with minimal keep-alive
	tr := &http.Transport{
		MaxIdleConns: 1,
	    IdleConnTimeout: 1 * time.Second,
	}
	client := &http.Client{Transport: tr}

	configHandler := http.HandlerFunc(ConfigHandler)
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

	if (config.Metrics.Enabled) {			
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

		wrappedIndexHandler := promhttp.InstrumentHandlerInFlight(inFlightGauge,
								promhttp.InstrumentHandlerCounter(requestCounter, indexHandler))
		
		http.Handle("/", wrappedIndexHandler)
		http.Handle("/metrics", promhttp.Handler())
	} else {
		http.Handle("/", indexHandler)
	}
	
	http.Handle("/config", configHandler)
	http.ListenAndServe(":80", nil)
}