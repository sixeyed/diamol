# using old versions for Windows compatibility
FROM diamol/golang:2e AS builder
ENV  GO111MODULE=auto
WORKDIR ${GOPATH}/src/github.com/docker
RUN git clone --depth 1 --branch v2.6.2 https://github.com/docker/distribution.git

WORKDIR ${GOPATH}/src/github.com/docker/distribution
RUN go build -o /out/registry ./cmd/registry

# Registry
FROM diamol/base:2e

WORKDIR /data
EXPOSE 5000
CMD ["/app/registry", "serve", "config.yml"]

WORKDIR /app
COPY --from=builder /out/registry .
COPY config.yml ./config.yml