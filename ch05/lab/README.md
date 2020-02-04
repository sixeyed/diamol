# DIAMOL Chapter 5 Lab - Sample Solution

[Registry v2 API docs](https://docs.docker.com/registry/spec/api)

> If you're using PowerShell on Windows, the `curl` command is an alias for `Invoke-WebRequest`, which has different behaviour from the real cURL. Recent versions of Windows 10 have cURL available as `curl.exe`, so I'd recommend using that instead (just add `.exe` to the commands below).

## Push

```
docker image push registry.local:5000/gallery/ui
```

> Pushes all tags for the repo

## Check 

```
curl http://registry.local:5000/v2/gallery/ui/tags/list
```

## Get manifest for `latest`

```
curl --head \
  http://registry.local:5000/v2/gallery/ui/manifests/latest \
  -H 'Accept: application/vnd.docker.distribution.manifest.v2+json'
```
> Output headers include `Docker-Content-Digest`, this is the manifest you need

e.g. 

```
Docker-Content-Digest: sha256:127d0ed6f7a8d148a39b7ea168c083694d030e2bffbda60cb53057e731114fbb
```

## Delete

```
curl -X DELETE \
  http://registry.local:5000/v2/gallery/ui/manifests/sha256:127d0ed6f7a8d148a39b7ea168c083694d030e2bffbda60cb53057e731114fbb
```

## Check 

```
curl http://registry.local:5000/v2/gallery/ui/tags/list
```