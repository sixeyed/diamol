FROM diamol/golang AS builder

WORKDIR ${GOPATH}/src/github.com/docker
RUN git clone https://github.com/docker/distribution.git

WORKDIR ${GOPATH}/src/github.com/docker/distribution
RUN git checkout "v2.6.2" && \
    go build -o /out/registry ./cmd/registry && \
    chmod +x /out/registry

# Registry
FROM diamol/base

WORKDIR /data
EXPOSE 5000
CMD ["/registry/registry", "serve", "config.yml"]

WORKDIR /registry
COPY --from=builder /out/registry .
COPY config.yml ./config.yml