FROM diamol/golang AS builder

WORKDIR /go/src/github.com/pingcap
RUN git clone --depth 1 https://github.com/sixeyed/tidb.git

WORKDIR /go/src/github.com/pingcap/tidb
RUN go build -o bin/tidb-server tidb-server/main.go

# TiDB
FROM diamol/base

EXPOSE 4000
VOLUME /data

WORKDIR /
ENTRYPOINT ["/tidb-server", "--path", "/data"]

COPY --from=builder /go/src/github.com/pingcap/tidb/bin/tidb-server /tidb-server