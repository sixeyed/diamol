# DIAMOL Chapter 16 Lab - Sample Solution

There are multiple problems with the original [Dockerfile](./Dockerfile) which stop it being multi-arch:

- it's pinned to a Linux-Intel image in the `FROM` instruction
- it uses Linux-specific commands in the `RUN` instructions
- it uses Linux-specific commands in the `CMD`
- it uses Linux-style file paths.

The solution in [Dockerfile.solution](./Dockerfile.solution) fixes that by doing three things:

- using a multi-arch image as the base
- using `WORKDIR` to create and switch directories rather than OS commands
- using an OS command in the `CMD` instruction which works in Linux and Windows.

You can build and run the solution using Docker on any of the supported arhchitectures for this book:

```
docker image build -t diamol/ch16-lab -f Dockerfile.solution .

docker container run diamol/ch16-lab
```
