FROM diamol/golang:2e AS builder

COPY go.mod main.go /go/
RUN go build -o /hello

# app
FROM diamol/base:2e

CMD ["/app/hello"]

WORKDIR /app
COPY --from=builder /hello .
RUN chmod +x hello