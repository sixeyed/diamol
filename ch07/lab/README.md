# DIAMOL Chapter 7 Lab - Sample Solution

## Dev environment

This configuration uses Sqlite for data storage and published the web app to port `8020`.

Run from this directory with:

```
docker compose -f docker-compose-dev.yml up -d
```

> Test at http://localhost:8020

## Test environment

```
mkdir -p /tmp/data/postgres

docker compose -f docker-compose-test.yml up -d
```

> Test at http://localhost:8050

Or on Windows:

```
mkdir -p /tmp/data/postgres

docker compose -f docker-compose-test-windows.yml up -d
```

> Test at http://localhost:8050