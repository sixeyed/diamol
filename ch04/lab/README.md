# DIAMOL Chapter 4 Lab - Sample Solution

## Before
docker image build -t ch04-lab .

docker container run -d -p 804:80 ch04-lab

image is 322MB on Linux; 5.2GB on Windows

## After
docker image build -t ch04-lab:optimized -f Dockerfile.optimized .

docker container run -d -p 805:80 ch04-lab:optimized

image is 24MB on Linux; 230MB on Windows

## Linux

```
>docker image ls 'ch04*'
REPOSITORY   TAG         IMAGE ID       CREATED              SIZE
ch04-lab     optimized   4f5dd978810d   About a minute ago   24.1MB
ch04-lab     latest      bb5d5c754db9   3 minutes ago        322MB
```

## Windows

```
>docker image ls ch04*
REPOSITORY          TAG                 IMAGE ID            CREATED              SIZE
ch04-lab            optimized           2f017c0f1524        About a minute ago   260MB
ch04-lab            latest              42013cf1495c        2 minutes ago        5.14GB
```
