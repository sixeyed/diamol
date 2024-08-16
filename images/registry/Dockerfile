# using old versions for Windows compatibility
FROM golang:1.12 AS builder
ENV CGO_ENABLED=0

WORKDIR ${GOPATH}/src/github.com/docker
RUN git clone https://github.com/docker/distribution.git

WORKDIR ${GOPATH}/src/github.com/docker/distribution
RUN git checkout "v2.6.2" && \
    go build -o /out/registry ./cmd/registry

# Registry
FROM diamol/base:2e

WORKDIR /data
EXPOSE 5000
CMD ["/app/registry", "serve", "config.yml"]

WORKDIR /app
COPY --from=builder /out/registry .
COPY config.yml ./config.yml