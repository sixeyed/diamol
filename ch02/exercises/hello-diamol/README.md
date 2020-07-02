
0. Build and push the arch images

1. Enable experimental in `~/.docker/config.json`:

```
{
  "experimental": "enabled"
}
```

2. Create the manifest list (multi-arch name followed by individual image names):

```
docker manifest create `
 diamol/ch02-hello-diamol:1.0 `
 diamol/ch02-hello-diamol:windows-x64 `
 diamol/ch02-hello-diamol:linux-x64 `
 diamol/ch02-hello-diamol:linux-amd64
```

3. Inspect & confirm archs:

```
docker manifest inspect diamol/ch02-hello-diamol:1.0
```

4. Push

```
docker manifest push diamol/ch02-hello-diamol:1.0
```