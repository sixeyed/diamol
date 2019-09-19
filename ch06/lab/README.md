# DIAMOL Chapter 6 Lab - Sample Solution

If you've been running lots of containers from Chapter 6 and using lots of ports, you can start by tidying up:

> This will remove **ALL** of your containers

```
docker container rm -f $(docker container ls -aq)
```

Now run a container with the lab's to-do list image:

```
docker container run -d -p 8015:80 diamol/ch06-lab
```

Browse to http://localhost:8015/list  - it should look like this:

![Sample to-do list with some important tasks](./todo-list-v3.png)

> A set of tasks which everyone should get done :)

Let's create a volume to use for storing the database file instead:

```
docker volume create ch06-lab
```

You can create a configuration file which specifies a different path for the database file, and that path can be your volume mount. 

My [config.json](./solution/config.json) configures the app to write the database file in `/new-data`.

To put that together, we'll run a container which uses:

- a read-only bind mount to load the new config file into the container
- a read-write volume mount as the target for the database file

Which is this set of paths on Windows:

```
$configSource="$(pwd)/solution".ToLower()
$configTarget='c:\app\config'
$dataTarget='c:\new-data'
```

And this on Linux:

```
configSource="$(pwd)/solution"
configTarget='/app/config'
dataTarget='/new-data'
```

And now you can run the container:

```
docker container run -d -p 8016:80 --mount type=bind,source=$configSource,target=$configTarget,readonly --volume ch06-lab:$dataTarget diamol/ch06-lab
```

And browse to http://localhost:8016/list

> You'll see an empty to-do list which you can endlessly fill