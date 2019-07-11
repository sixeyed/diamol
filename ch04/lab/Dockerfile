FROM diamol/golang 
WORKDIR web
COPY index.html .
COPY main.go .
RUN go build -o ./server
RUN chmod +x ./server
CMD ["/web/server"]
ENV USER=sixeyed
EXPOSE 80