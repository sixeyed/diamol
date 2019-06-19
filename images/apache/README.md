
0. Build and push the arch images:

- Linux/amd64
- Linux/arm64
#- Linux/arm
- Windows/amd64

1. Enable experimental in `~/.docker/config.json`:

```
{
  "experimental": "enabled"
}
```

2. Create the manifest list (multi-rach name followed by individual image names):

```
docker manifest create `
 diamol/apache `
 diamol/apache:windows-amd64 `
 diamol/apache:linux-amd64 `
 diamol/apache:linux-arm64 `
#diamol/apache:linux-arm
```

3. Inspect & confirm archs:

```
docker manifest inspect diamol/apache
```

4. Push

```
docker manifest push diamol/apache
```