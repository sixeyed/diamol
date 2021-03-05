# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# VC++ redistributables
RUN Invoke-WebRequest -OutFile VC_redist.x64.exe 'aka.ms/vs/16/release/VC_redist.x64.exe'
RUN Start-Process .\VC_redist.x64.exe -ArgumentList '/install /passive /norestart' -Wait

ARG HTTPD_VERSION="2.4.46"
RUN Write-Host "Downloading Apache version: $env:HTTPD_VERSION"; `
    Invoke-WebRequest -OutFile httpd.zip "https://fossies.org/windows/www/httpd-$($env:HTTPD_VERSION)-win64-VS16.zip"; `
    Expand-Archive httpd.zip -DestinationPath C:\ ;

# Apache
FROM diamol/base

EXPOSE 80 443
WORKDIR C:\Apache24
CMD ["bin\\httpd.exe", "-DFOREGROUND"]

COPY --from=installer C:\Apache24\ .
COPY --from=installer C:\windows\system32\msvcp140.dll C:\windows\system32
COPY --from=installer C:\windows\system32\vcruntime140.dll C:\windows\system32
COPY httpd.conf C:\Apache24\conf\httpd.conf
COPY index.html /usr/local/apache2/htdocs/