# escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS installer
ARG LIBRESSL_VERSION
ARG HTTPD_BUILD
RUN echo "Downloading LibreSSL version: %LIBRESSL_VERSION%" && `
    curl -sSL -o libressl.zip "https://ftp.openbsd.org/pub/OpenBSD/LibreSSL/libressl-%LIBRESSL_VERSION%-windows.zip"
WORKDIR /libressl
RUN tar -xf /libressl.zip --strip-components 1

# Cert generator
FROM diamol/base:2e

ARG LIBRESSL_VERSION
ENV LIBRESSL_HOME C:\libressl

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\libressl;"

WORKDIR ${LIBRESSL_HOME}
COPY --from=installer C:\libressl\x64\ .
COPY openssl.cnf .\ssl\openssl.cnf

COPY generate-certs.bat C:\

ENV HOST_NAME="localhost" `
  HOST_IP="127.0.0.1" `
  EXPIRY_DAYS=730

WORKDIR C:\certs
CMD C:\generate-certs.bat ${HOST_NAME} ${HOST_IP} ${EXPIRY_DAYS}