docker-compose -f docker-compose.yml -f docker-compose-build.yml push

docker image tag $REGISTRY/diamol/ch15-timecheck:3.0-build-$BUILD_NUMBER sixeyed/diamol-ch15-timecheck:3.0

docker login -u $hub_user -p $hub_password

docker image push sixeyed/diamol-ch15-timecheck:3.0