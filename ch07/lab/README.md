# DIAMOL Chapter 7 Lab - Sample Solution

This solution uses [multiple Docker Compose files](https://docs.docker.com/compose/extends/) to share core the app definition across multiple environments, without duplication.

## Dev environment

This configuration uses Sqlite for data storage and published the web app to port `8040`.

Run from this directory with:

```
docker-compose -f docker-compose-core.yml -f docker-compose-dev.yml up -d
```

## Test environment


```
docker-compose -f docker-compose-core.yml -f docker-compose-test.yml up -d
```

Or on Windows:

```
docker-compose -f docker-compose-core.yml -f docker-compose-test.yml -f docker-compose-test-windows.yml up -d
```