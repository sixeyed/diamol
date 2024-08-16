FROM diamol/golang:2e AS builder
COPY go.mod main.go /go/
RUN go build -o /gols

# app
FROM diamol/base:2e

ENTRYPOINT ["/gols"]
CMD ["/init"]

COPY --from=builder /gols /
RUN chmod +x /gols

WORKDIR /init
COPY ./existing/ .