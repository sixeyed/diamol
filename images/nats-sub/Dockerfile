FROM diamol/golang:2e AS builder

COPY go.mod nats-sub.go /src/
WORKDIR /src
RUN go mod tidy
RUN go build -o /nats-sub

# app
FROM diamol/base:2e
ENTRYPOINT ["/app/nats-sub", "-s", "nats://message-queue:4222"]

WORKDIR /app
COPY --from=builder /nats-sub .
RUN chmod +x nats-sub