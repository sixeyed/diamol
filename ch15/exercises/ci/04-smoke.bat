echo "Deploying to $SMOKE_ENGINE; with CA: $ca, cert: $cert; key: $key"
docker-compose --host "tcp://$SMOKE_ENGINE" --tlsverify --tlscacert $ca --tlscert $cert --tlskey $key -f docker-compose.yml -f docker-compose-smoketest.yml up -d
