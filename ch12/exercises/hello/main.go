package main

import (
    "fmt"
    "runtime"
)

func main() {
    fmt.Println("Hello, World! I am running on:", runtime.GOOS, runtime.GOARCH)
}