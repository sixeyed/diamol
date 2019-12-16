FROM diamol/golang AS builder

WORKDIR /src
COPY ./src/go.mod .
RUN go mod download

COPY ./src/main.go .
RUN go build -o /server

# app
FROM diamol/base

EXPOSE 80
CMD ["/app/server"]

WORKDIR /app
COPY ./src/index.html .
COPY ./src/config.toml .
COPY --from=builder /server .
RUN chmod +x server