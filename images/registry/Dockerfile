FROM diamol/golang AS builder

WORKDIR /go/src/github.com/docker
RUN git clone https://github.com/docker/distribution.git

WORKDIR /go/src/github.com/docker/distribution
RUN git checkout "v2.6.2"; \
    go build -o /out/registry ./cmd/registry; \
    chmod +x /out/registry

# Registry
FROM diamol/base

WORKDIR /data
ENV REGISTRY_STORAGE_FILESYSTEM_ROOTDIRECTORY="/data"
EXPOSE 5000
CMD ["/registry/registry", "serve", "config.yml"]

WORKDIR /registry
COPY --from=builder /out/registry .
COPY --from=builder /go/src/github.com/docker/distribution/cmd/registry/config-example.yml ./config.yml