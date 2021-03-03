FROM diamol/golang AS builder

COPY main.go .
RUN go build -o /server

# app
FROM diamol/base

# add CURL for healthchecks in chapter 14
RUN apk add --no-cache curl

ENV IMAGE_API_URL="http://iotd/image" \
    ACCESS_API_URL="http://accesslog/access-log"

CMD ["/web/server"]

WORKDIR /web
COPY index.html .
COPY --from=builder /server .
RUN chmod +x server