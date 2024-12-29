#escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG GOGS_VERSION
ARG GOGS_WINDOWS_DOWNLOAD_SHA256

RUN Write-Host "Downloading Gogs version: $($env:GOGS_VERSION)"; `
    Invoke-WebRequest -Uri "https://dl.gogs.io/$($env:GOGS_VERSION)/gogs_$($env:GOGS_VERSION)_windows_amd64.zip" -OutFile 'gogs.zip';

RUN if ((Get-FileHash gogs.zip -Algorithm sha256).Hash.ToLower() -ne $env:GOGS_WINDOWS_DOWNLOAD_SHA256) {exit 1}

RUN Write-Host 'Expanding Gogs...'; `
    Expand-Archive gogs.zip -DestinationPath C:\;

# Git
ARG WINDOWS_VERSION
FROM diamol/git-windows:2e-windows-${WINDOWS_VERSION}-amd64 AS git

# Gogs
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

EXPOSE 3000
VOLUME C:\data C:\logs C:\repositories
CMD ["gogs", "web"]

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;"

WORKDIR /git
COPY --from=git /git/ .

WORKDIR /gogs
COPY --from=installer /gogs .
COPY app.ini ./custom/conf/app.ini

