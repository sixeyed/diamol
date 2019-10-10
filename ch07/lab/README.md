# DIAMOL Chapter 7 Lab - Sample Solution

## Dev environment

This configuration uses Sqlite for data storage and published the web app to port `8020`.

Run from this directory with:

```
docker-compose -f docker-compose-dev.yml up -d
```

## Test environment

```
mkdir -p /data/postgres

docker-compose -f docker-compose-test.yml up -d
```

Or on Windows:

```
mkdir -p /data/postgres

docker-compose -f docker-compose-test-windows.yml up -d
```
