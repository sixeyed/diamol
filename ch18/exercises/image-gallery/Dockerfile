FROM diamol/golang AS builder

WORKDIR /src
COPY go.mod .
RUN go mod download

COPY main.go .
RUN go build -o /server

# app
FROM diamol/base

CMD ["/web/server"]

WORKDIR /web
COPY index.html .
COPY config.toml .
COPY --from=builder /server .
RUN chmod +x server