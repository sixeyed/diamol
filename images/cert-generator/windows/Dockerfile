# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG LIBRESSL_VERSION="2.5.5"

RUN Write-Host "Downloading LibreSSL version: $env:LIBRESSL_VERSION"; `
  Invoke-WebRequest "https://ftp.openbsd.org/pub/OpenBSD/LibreSSL/libressl-$env:LIBRESSL_VERSION-windows.zip" -OutFile C:\libressl.zip -UseBasicParsing

RUN Expand-Archive C:\libressl.zip C:\; `
  Remove-Item "C:\libressl-$env:LIBRESSL_VERSION-windows\x64\*.pdb"

# Cert generator
FROM diamol/base

ARG LIBRESSL_VERSION="2.5.5"
ENV LIBRESSL_HOME C:\libressl

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\libressl;"

WORKDIR ${LIBRESSL_HOME}
COPY --from=installer C:\libressl-${LIBRESSL_VERSION}-windows\x64\ .
COPY openssl.cnf .\ssl\openssl.cnf

COPY generate-certs.bat C:\

ENV HOST_NAME="localhost" `
  HOST_IP="127.0.0.1" `
  EXPIRY_DAYS=730

WORKDIR C:\certs
CMD C:\generate-certs.bat ${HOST_NAME} ${HOST_IP} ${EXPIRY_DAYS}