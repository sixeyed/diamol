FROM diamol/golang:2e AS builder

COPY go.mod main.go /go/
RUN go build -o /server

# app
FROM diamol/base:2e
ENV IMAGE_API_URL="http://iotd/image" \
    ACCESS_API_URL="http://accesslog/access-log"

CMD ["/web/server"]

WORKDIR /web
COPY index.html .
COPY --from=builder /server .
RUN chmod +x server