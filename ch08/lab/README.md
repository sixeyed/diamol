# DIAMOL Chapter 8 Lab - Sample Solution

The original app is packaged using this [Dockerfile](./Dockerfile). The solution adds a dependency check and a health check in [Dockerfile.solution](./Dockerfile.solution).

## Usage

You can build the solution from my Dockerfile:

```
docker image build -t diamol/ch08-lab:solution -f Dockerfile.solution .
```

And run the container interactively; the app will print out its (fake) memory allocations:

```
docker container run diamol/ch08-lab:solution
```

> Check the container list after a while to see the health, and inspect the container to see the health check output

## Dependency check

At startup the container should run the `memory-check.js` script to see there's enough memory for the app to run. If there is then the app can start, if not the container should exit.

You can do this in a cross-platform way in the `CMD` instruction:

```
CMD node memory-check.js && \
    node memory-hog.js
```

## Health check

At five-second intervals the same `memory-check.js` script can be run in a health check to ensure the app hasn't breached its memory limit:

```
HEALTHCHECK --interval=5s \
 CMD node memory-check.js
```
