FROM golang:1.12.6 AS builder
COPY src /src
WORKDIR /src
ENV CGO_ENABLED=0
RUN go build -o /webserver

# webserver
FROM diamol/base
EXPOSE 80
CMD ["webserver.exe"]

WORKDIR /usr/bin
COPY --from=builder /webserver ./webserver.exe
RUN chmod +x webserver.exe

COPY html /html