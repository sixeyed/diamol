## Credits

From Anchore's docs: https://docs.anchore.com/current/docs/engine/engine_installation/docker_compose/

Using Anchore Engine version 0.5.2:

```
docker create --name ae anchore/anchore-engine:v0.5.2

docker cp ae:/docker-compose.yaml ./docker-compose.yaml

docker rm -f ae
```