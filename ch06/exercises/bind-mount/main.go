package main

import (	
	"io/ioutil"
	"fmt"
	"os"
)

func main() {
	root := "/init"
	if len(os.Args) > 1 {
		root = os.Args[1];
	}

 	fileInfo,_ := ioutil.ReadDir(root)
	for _, file := range fileInfo {
		fmt.Println(file.Name()) 	 	
	}	 
}