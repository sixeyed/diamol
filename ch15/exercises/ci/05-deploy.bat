docker-compose --host tcp://localhost:2376 --tlsverify --tlscacert $ca --tlscert $cert --tlskey $key -p timecheck-pro -f docker-compose.yml -f docker-compose-prod.yml up -d
