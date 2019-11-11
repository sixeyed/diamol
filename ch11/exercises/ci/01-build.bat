echo 'Building numbers...'
docker-compose -f docker-compose.yml -f docker-compose-build.yml build --pull