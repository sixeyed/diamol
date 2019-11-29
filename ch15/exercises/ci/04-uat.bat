echo "Deploying to $UAT_ENGINE"
docker-compose --host tcp://$UAT_ENGINE --tlsverify --tlscacert $ca --tlscert $cert --tlskey $ke -p timecheck-uat -f docker-compose.yml -f docker-compose-uat.yml up -d
