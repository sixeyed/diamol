# DIAMOL Chapter 10 Lab - Sample Solution

## Dev

```
docker-compose up -d
```

## Test

```
docker-compose -f .\docker-compose.yml -f .\docker-compose-test.yml -p ch10-lab-test up -d
```

> Data persists between `up` and `down` as the database container uses a volume.
