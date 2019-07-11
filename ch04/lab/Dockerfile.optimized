FROM diamol/golang AS builder

COPY main.go .
RUN go build -o /server
RUN chmod +x /server

# app
FROM diamol/base

EXPOSE 80
CMD ["/web/server"]
ENV USER="sixeyed"

WORKDIR web
COPY --from=builder /server .
COPY index.html .