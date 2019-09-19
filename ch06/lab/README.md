# DIAMOL Chapter 6 Lab - Sample Solution

If you've been running lots of containers from Chapter 6 and using lots of ports, you can start by tidying up:

```
docker container rm -f $(docker container ls -aq)
```

Now run a container with the latest to-do list image:

```
docker container run -d -p 8015:80 diamol/ch06-todo-list:v3
```

Browse to http://localhost:8015/list  - it should look like this:

![Sample to-do list with some inportant tasks](./todo-list-v3.png)

> A set of tasks which everyone should get done :)