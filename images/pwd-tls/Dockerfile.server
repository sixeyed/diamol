FROM diamol/base

WORKDIR /pwd
COPY daemon.json .
ADD certs .

CMD mkdir -p /certs && \    
    cp ca.pem /certs && \
    cp server-*.pem /certs && \
    cp /docker/daemon.json /docker/daemon.json.bak && \
    cp daemon.json /docker/daemon.json