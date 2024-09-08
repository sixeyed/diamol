# DIAMOL Chapter 10 Lab - Sample Solution

You can run the development and test deployments at the same time.

Start from the `ch10/lab` folder.

## Dev

```
docker compose up -d
```

> Test at http://localhost:8089

This deployment uses a local SQLite database file inside the container. If you restart the app then the data is lost.

## Test

```
docker compose -f docker-compose.yml -f docker-compose-test.yml -p ch10-lab-test up -d
```
> Test at http://localhost:8080

This deployment runs a separate Postgres database container for storage. Data persists between `up` and `down` as the database container uses a volume.
