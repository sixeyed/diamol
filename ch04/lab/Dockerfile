FROM diamol/golang:2e

WORKDIR /web
COPY index.html .
COPY go.mod main.go /web/

RUN go build -o /web/server
RUN chmod +x /web/server

CMD ["/web/server"]
ENV USER=sixeyed
EXPOSE 80