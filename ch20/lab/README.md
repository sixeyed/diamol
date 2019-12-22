## Before

docker-compose -f .\pi\docker-compose.yml up -d

http://localhost:8031/?dp=50000

## After

pi.local >> hosts

docker-compose -f .\solution\docker-compose.yml up -d
