#!/bin/sh

IP="$2"
if [ "$IP" = "" ]; then 
    IP='127.0.0.1'
fi
if [ "$IP" = "localhost" ]; then 
    IP='127.0.0.1'
fi

echo "$IP  $1" | sudo tee -a /etc/hosts