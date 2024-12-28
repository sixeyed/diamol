ARG WINDOWS_VERSION=ltsc2025
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG ZIP_URL=https://raw.githubusercontent.com/sixeyed/diamol/refs/heads/2e/ch13/exercises/aspnet/aspnet-app.zip
ARG HASH_URL=https://raw.githubusercontent.com/sixeyed/diamol/refs/heads/2e/ch13/exercises/aspnet/aspnet-app.zip.sha256

WORKDIR /install

RUN curl.exe -sSL -o app.zip $env:ZIP_URL; \
    curl.exe -sSL -o sha256 $env:HASH_URL; \
    $expectedHash = Get-Content -Path sha256; \
    $actualHash = (Get-FileHash -Algorithm SHA256 -Path app.zip).Hash; \
    if ($actualHash -ne $expectedHash) { exit 1 }; \
    Expand-Archive -Path app.zip -DestinationPath /app -Force

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

ENV DIAMOL_KEY_1="first value" \
    DIAMOL_KEY_2="second value"

WORKDIR /inetpub/wwwroot
COPY --from=installer /app/ ./