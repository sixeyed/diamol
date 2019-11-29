echo "Deploying to $PROD_ENGINE"
docker-compose --host tcp://$PROD_ENGINE --tlsverify --tlscacert $ca --tlscert $cert --tlskey $key -p timecheck_prod -f docker-compose.yml -f docker-compose-prod.yml up -d
