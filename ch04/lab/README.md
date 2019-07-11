# DIAMOL Chapter 4 Lab - Sample Solution

## Before
docker image build -t ch04-lab .

docker container run -d -p 804:80 ch04-lab

image is 800MB on Linux; 5.2GB on Windows

## After
docker image build -t ch04-lab:optimized -f Dockerfile.optimized .

docker container run -d -p 805:80 ch04-lab:optimized

image is 15MB on Linux; 230MB on Windows

## Linux

```
>docker image ls ch04*
REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE 
ch04-lab            optimized           acd8afedcb0d        16 minutes ago      15.3MB
ch04-lab            latest              87d6bce2a950        19 minutes ago      802MB
```

## Windows

```
>docker image ls ch04*
REPOSITORY          TAG                 IMAGE ID            CREATED              SIZE
ch04-lab            optimized           2f017c0f1524        About a minute ago   260MB
ch04-lab            latest              42013cf1495c        2 minutes ago        5.14GB
```
