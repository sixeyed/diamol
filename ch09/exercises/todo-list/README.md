# To-Do List - using Server-Side Blazor

## Building on Windows
 
You need to specify a C-drive path for the volume:

```
docker image build -t diamol/todo-blazor --build-arg DATA_DIRECTORY=C:/data .
```

## Sharing volumes

Run a container:

```
docker container run --name todo1 -it diamol/ch06-todo-list
```

Add some items to the to-do list.

Run another container:

```
docker container run --name todo2 -it diamol/todo-blazor
```

Check that the to-do list is empty.

Now run a container using the first container's volume:

```
docker container run --name todo3 --volumes-from todo1 -it diamol/todo-blazor
```

Check the to-do list; it is from `todo1`.
