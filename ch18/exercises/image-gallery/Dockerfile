FROM diamol/golang:2e AS builder

COPY src/go.mod src/main.go /src/
WORKDIR /src
RUN go mod tidy
RUN go build -o /server

# app
FROM diamol/base:2e

EXPOSE 80
CMD ["/app/server"]

WORKDIR /app
COPY src/index.html .
COPY src/config.toml .
COPY --from=builder /server .
RUN chmod +x server