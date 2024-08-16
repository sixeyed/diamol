# escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# VC++ redistributables
RUN Invoke-WebRequest -OutFile VC_redist.x64.exe 'https://aka.ms/vs/17/release/VC_redist.x64.exe'
RUN Start-Process .\VC_redist.x64.exe -ArgumentList '/install /passive /norestart' -Wait

ARG HTTPD_VERSION
ARG HTTPD_BUILD
RUN Write-Host "Downloading Apache version: $env:HTTPD_VERSION"; `
    curl.exe -sSL -o httpd.zip "https://www.apachelounge.com/download/VS17/binaries//httpd-$($env:HTTPD_VERSION)-$($env:HTTPD_BUILD)-win64-VS17.zip"; `
    Expand-Archive httpd.zip -DestinationPath C:\ ;

# Apache
FROM diamol/base:2e

EXPOSE 80 443
WORKDIR C:\Apache24
CMD ["bin\\httpd.exe", "-DFOREGROUND"]

COPY --from=installer C:\Apache24\ .
COPY --from=installer C:\windows\system32\msvcp140.dll C:\windows\system32
COPY --from=installer C:\windows\system32\vcruntime140.dll C:\windows\system32
COPY httpd.conf C:\Apache24\conf\httpd.conf
COPY index.html /usr/local/apache2/htdocs/