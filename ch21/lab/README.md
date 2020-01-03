# DIAMOL Chapter 21 Lab - Sample Solution

Add a message handler to the to-do app which mutates data, editing the text users have saved.

## Configuration

My [Docker Compose](./docker-compose.yml) file is here with the sample solution. I joined together all the override files from the exercise folder as my starting point.

You need to set an environment variable for the save message handler, which enables the feature for it to publish "event-saved" messages (the clue for that setting is in the [save handler Dockerfile](../exercises/todo-list/src/save-handler/Dockerfile)):

```
  save-handler:
    image: diamol/ch21-save-handler
    environment:
      - Events__events.todo.itemsaved__Publish=true
    networks:
      - app-net
```

And you need a new service for the mutating message handler, which needs the correct URL for the message queue (the clue for that is in the app's [default configuration file](../exercises/todo-list/src/mutating-handler/appsettings.json)):

```
  mutating-handler:
    image: diamol/ch21-mutating-handler
    environment:
      - MessageQueue__Url=nats://message-queue:4222
    networks:
      - app-net
```
## Try it out

Run `docker-compose up -d` from this directory. 

You can check all the handlers are listening on the queue:

```
docker container logs lab_save-handler_1
docker container logs lab_audit-handler_1
docker container logs lab_mutating-handler_1
```

Now browse to http://localhost:8081/new and add a new entry to your to-do list. Wait a moment for all the handlers to fire, and then refresh the list. When the updates have happened (remember eventual consistency) then you'll see your to-do item has been changed to something very worthwhile:

![Mutating to-do items with a message handler](./solution.png)
