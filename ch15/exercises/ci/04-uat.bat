echo "Deploying to $UAT_ENGINE"
docker-compose -f docker-compose.yml -f docker-compose-uat.yml config
docker-compose --host tcp://$UAT_ENGINE --tlsverify --tlscacert $ca --tlscert $cert --tlskey $key -f docker-compose.yml -f docker-compose-uat.yml up -d
