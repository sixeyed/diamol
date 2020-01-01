# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG NGINX_VERSION="1.17.6"

RUN Write-Host "Downloading Nginx version: $env:NGINX_VERSION"; `
    Invoke-WebRequest -OutFile nginx.zip -UseBasicParsing "http://nginx.org/download/nginx-$($env:NGINX_VERSION).zip"; `
    Expand-Archive nginx.zip -DestinationPath C:\ ; `
    Rename-Item "C:\nginx-$($env:NGINX_VERSION)" C:\nginx;

# NGINX - requires server core because the binary is 32/64 bit and doesn't run in nano
FROM mcr.microsoft.com/windows/servercore:ltsc2019

ARG NGINX_VERSION="1.17.6"
ENV NGINX_HOME="C:\etc\nginx"

EXPOSE 80 443
WORKDIR ${NGINX_HOME}
ENTRYPOINT ["nginx.exe"]

RUN mkdir \data\nginx\cache\long \data\nginx\cache\short
COPY --from=installer C:\nginx\ .
COPY nginx.conf ${NGINX_HOME}\