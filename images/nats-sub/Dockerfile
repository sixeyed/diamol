FROM diamol/golang AS builder

WORKDIR /src
COPY go.mod .
RUN go mod download

COPY nats-sub.go .
RUN go build -o /nats-sub

# app
FROM diamol/base
ENTRYPOINT ["/app/nats-sub", "-s", "nats://message-queue:4222"]

WORKDIR /app
COPY --from=builder /nats-sub .
RUN chmod +x nats-sub