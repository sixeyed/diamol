
0. Build and push the arch images:

- Linux/amd64
- Linux/arm64
- Linux/arm
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
 diamol/base `
 diamol/base:windows-amd64 `
 diamol/base:linux-amd64 `
 diamol/base:linux-arm64 `
 diamol/base:linux-arm
```

3. Inspect & confirm archs:

```
docker manifest inspect diamol/base
```

4. Push

```
docker manifest push diamol/base
```