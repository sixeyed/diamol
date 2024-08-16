# DIAMOL Chapter 5 Lab - Sample Solution

[Registry v2 API docs](https://github.com/opencontainers/distribution-spec/blob/v1.0.1/spec.md)

> If you're using PowerShell on Windows, the `curl` command is an alias for `Invoke-WebRequest`, which has different behaviour from the real cURL. Recent versions of Windows have cURL available as `curl.exe`, so I'd recommend using that instead (just add `.exe` to the commands below).

## Push

You can push multiple images for one repo using `all-tags`:

```
docker image push --all-tags registry.local:5010/gallery/ui
```

## Check the image tags

See https://github.com/opencontainers/distribution-spec/blob/v1.0.1/spec.md#content-discovery

```
curl http://registry.local:5010/v2/gallery/ui/tags/list
```

Should give you one repo with multiple tags:

```
{"name":"gallery/ui","tags":["2","2.1.106","latest","v1","2.1"]}
```

## Get manifest for `latest`

```
curl --head `
  http://registry.local:5010/v2/gallery/ui/manifests/latest `
  -H 'Accept: application/vnd.docker.distribution.manifest.v2+json'
```
> Output headers include `Docker-Content-Digest`, this is the manifest you need

e.g. 

```
Docker-Content-Digest: sha256:ee332f847543d675155772f3a15ba9f788fe2823e832efd77b9fb36ffcb32f82
```

## Delete

```
curl -XDELETE `
  http://registry.local:5010/v2/gallery/ui/manifests/sha256:ee332f847543d675155772f3a15ba9f788fe2823e832efd77b9fb36ffcb32f82
```

## Check 

```
curl http://registry.local:5010/v2/gallery/ui/tags/list
```

The repository is still listed, but there are no tags - deleting the image removes all the tags which were associated with it:

```
{"name":"gallery/ui","tags":null}
```
