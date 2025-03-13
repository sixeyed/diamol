# escape=`
ARG WINDOWS_VERSION
FROM diamol/windows-deps:2e-windows-${WINDOWS_VERSION}-amd64 AS windows-deps
    
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS httpd
ARG HTTPD_VERSION
ARG HTTPD_BUILD
USER ContainerAdministrator
RUN echo "Downloading Apache version: %HTTPD_VERSION% build: %HTTPD_BUILD%" && `
    curl -sSL -o httpd.zip "https://www.apachelounge.com/download/VS17/binaries//httpd-%HTTPD_VERSION%-%HTTPD_BUILD%-win64-VS17.zip" && `
    tar -xf httpd.zip

# Apache
FROM diamol/base:2e

EXPOSE 80 443
WORKDIR C:\Apache24
CMD ["bin\\httpd.exe", "-DFOREGROUND"]

COPY --from=httpd C:\Apache24\ .
COPY --from=windows-deps C:\windows\system32\msvcp140.dll C:\windows\system32
COPY --from=windows-deps C:\windows\system32\vcruntime140.dll C:\windows\system32
COPY httpd.conf C:\Apache24\conf\httpd.conf
COPY index.html /usr/local/apache2/htdocs/