#escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS base

FROM base AS installer
ARG GOGS_VERSION
ARG GOGS_WINDOWS_DOWNLOAD_SHA256

RUN echo "Downloading Gogs version: %GOGS_VERSION%" && `
    curl -sSL -o gogs.zip "https://dl.gogs.io/%GOGS_VERSION%/gogs_%GOGS_VERSION%_windows_amd64.zip"

# TODO - use this for checksum? https://github.com/cwansart/sha256sum.exe
# RUN if ((Get-FileHash gogs.zip -Algorithm sha256).Hash.ToLower() -ne $env:GOGS_WINDOWS_DOWNLOAD_SHA256) {exit 1}

RUN echo 'Expanding Gogs...' && `
    mkdir gogs && `
    cd gogs && `
    tar -xvf ../gogs.zip

# Git
ARG WINDOWS_VERSION
FROM diamol/windows-deps:2e-windows-${WINDOWS_VERSION}-amd64 AS windows-deps

# Gogs
FROM base

EXPOSE 3000
VOLUME C:\data C:\logs C:\repositories
CMD ["gogs", "web"]

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;"

WORKDIR /git
COPY --from=windows-deps /git/ .

WORKDIR /gogs
COPY --from=installer /gogs .
COPY app.ini ./custom/conf/app.ini

