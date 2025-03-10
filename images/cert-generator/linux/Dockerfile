FROM diamol/base:2e

RUN apk add --no-cache openssl

COPY generate-certs.sh /
RUN chmod +x /generate-certs.sh

ENV HOST_NAME="localhost" \
    HOST_IP="127.0.0.1" \
    EXPIRY_DAYS=730

WORKDIR /certs
CMD /generate-certs.sh ${HOST_NAME} ${HOST_IP} ${EXPIRY_DAYS}