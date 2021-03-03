#escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 as installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG GOGS_VERSION="0.11.91"

RUN Write-Host "Downloading Gogs version: $($env:GOGS_VERSION)"; `
    Invoke-WebRequest -Uri "https://dl.gogs.io/$($env:GOGS_VERSION)/gogs_$($env:GOGS_VERSION)_windows_amd64.zip" -OutFile 'gogs.zip';

RUN Write-Host 'Expanding Gogs...'; `
    Expand-Archive gogs.zip -DestinationPath C:\;

# Gogs
FROM diamol/base

EXPOSE 3000
VOLUME C:\data C:\logs C:\repositories
CMD ["gogs", "web"]

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;"

WORKDIR /git
COPY --from=diamol/git-windows /git/ .

WORKDIR /gogs
COPY --from=installer /gogs .
COPY app.ini ./custom/conf/app.ini

