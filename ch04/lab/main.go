package main

import (
	"html/template"
	"net/http"
	"os"
)

type Data struct {
	User string
}

func main() {
	tmpl := template.Must(template.ParseFiles("index.html"))

	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {

		data := Data{
			User: os.Getenv("USER"),
		}
		tmpl.Execute(w, data)
	})

	http.ListenAndServe(":80", nil)
}