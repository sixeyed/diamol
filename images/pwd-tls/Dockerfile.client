FROM diamol/base

WORKDIR /pwd
ADD certs .

CMD mkdir -p /certs && \
    cp ca.pem /certs && \
    cp client-*.pem /certs 