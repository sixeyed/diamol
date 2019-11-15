# DIAMOL Chapter 11 Lab - Sample Solution

## Build override file

- create `docker-compose-build.yml` with this content:

```
version: "3.7"

services:
  todo-web:
    build:
      context: todo-list
      dockerfile: Dockerfile
      args:
        BUILD_NUMBER: ${BUILD_NUMBER:-0}
```

## Jenkins job

- log into Jenkins

- New Item, copy from `diamol`

- change script path to ch11/lab/Jenkinsfile

- click Build Now

- check that the image has been pushed to the registry:

```
curl http://registry.local:5000/v2/diamol/ch11-todo-list/tags/list
```
