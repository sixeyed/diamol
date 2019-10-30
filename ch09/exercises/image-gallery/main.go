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

	tr := &http.Transport{
		MaxIdleConns: 1,
	    IdleConnTimeout: 1 * time.Second,
	}
	client := &http.Client{Transport: tr}

	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {

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
	
	http.Handle("/metrics", promhttp.Handler())
	
	http.ListenAndServe(":80", nil)
}