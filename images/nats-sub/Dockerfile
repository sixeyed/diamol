FROM diamol/golang AS builder

WORKDIR /src
COPY go.mod .
RUN go mod download

COPY nats-sub.go .
RUN go build -o /nats-sub

# app
FROM diamol/base

CMD ["nats-sub"]
COPY --from=builder /nats-sub .
RUN chmod +x nats-sub