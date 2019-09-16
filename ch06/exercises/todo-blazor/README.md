
## Building on Windows
 
You need to specify a C-drive path for the volume:

```
docker image build -t diamol/ch06-todo --build-arg DATA_DIRECTORY=C:/data .
```

## Sharing volumes

Run a container:

```
docker container run --name todo1 -it diamol/ch06-todo
```

Add some items to the to-do list.

Run another container:

```
docker container run --name todo2 -it diamol/ch06-todo
```

Check that the to-do list is empty.

Now run a container using the first container's volume:

```
docker container run --name todo3 --volumes-from todo1 -it diamol/ch06-todo
```

Check the to-do list; it is from `todo1`.
