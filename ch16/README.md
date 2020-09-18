# Addendum

## 16.4 - Building multi-arch images with Docker Buildx

The latest version of Docker does not work correctly with the version of Buildx used in the Play-with-Docker exercises.

On page 311 replace the first command (which uses Buildx 3.1) with this command to use the latest version:

```
# download the latest Buildx binary:

wget -O ~/.docker/cli-plugins/docker-buildx https://github.com/docker/buildx/releases/download/v0.4.2/buildx-v0.4.2.linux-amd64
```

And the rest of the exercises will work correctly.