echo "Deploying to $SMOKE_ENGINE"
stat $ca
stat $cert
stat $key
docker-compose -f docker-compose.yml -f docker-compose-smoketest.yml config
docker-compose --host tcp://$SMOKE_ENGINE --tlsverify --tlscacert $ca --tlscert $cert --tlskey $key -f docker-compose.yml -f docker-compose-smoketest.yml up -d
