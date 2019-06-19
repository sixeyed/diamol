package main

import (
	"fmt"
	"net/http"
)

func main() {
	fmt.Println("Serving content from /html on port 80")
	panic(http.ListenAndServe(fmt.Sprintf(":%s", "80"), http.FileServer(http.Dir("/html"))))
}