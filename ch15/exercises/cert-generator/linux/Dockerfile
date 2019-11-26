FROM diamol/base

RUN apk add --no-cache libressl

COPY generate-certs.sh /
RUN chmod +x /generate-certs.sh

ENV HOST_NAME="" \
    HOST_IP="" \
    EXPIRY_DAYS=730

WORKDIR /certs
CMD /generate-certs.sh ${HOST_NAME} ${HOST_IP} ${EXPIRY_DAYS}