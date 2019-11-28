docker-compose --host tcp://localhost:2376 --tlsverify --tlscacert $ca --tlscert $cert --tlskey $key -f docker-compose.yml -f docker-compose-smoketest.yml up -d
