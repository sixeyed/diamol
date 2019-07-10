package main

import (
	"bytes"
	"encoding/json"
	"html/template"
	"io/ioutil"	
	"net/http"
	"os"
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

	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {

		response,_ := http.Get(imageApiUrl)
		defer response.Body.Close()
		data,_ := ioutil.ReadAll(response.Body)
		image := Image{}
		json.Unmarshal([]byte(data), &image)	
		tmpl.Execute(w, image)

		log := AccessLog{
			ClientIP: r.RemoteAddr,
		}
		jsonLog,_ := json.Marshal(log)
		http.Post(logApiUrl, "application/json", bytes.NewBuffer(jsonLog))
	})

	http.ListenAndServe(":80", nil)
}