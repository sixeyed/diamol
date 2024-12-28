ARG WINDOWS_VERSION=ltsc2025
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR /install
RUN curl.exe -sSL -k -o aspnet-app.zip https://git.sixeyed/sixeyed/diamol-wip/raw/master/ch13/exercises/aspnet/aspnet-app.zip; \
    curl.exe -sSL -k -o aspnet-app.zip.sha256 https://git.sixeyed/sixeyed/diamol-wip/raw/master/ch13/exercises/aspnet/aspnet-app.zip.sha256; \
    $expectedHash = Get-Content -Path aspnet-app.zip.sha256; \
    $actualHash = (Get-FileHash -Algorithm SHA256 -Path aspnet-app.zip).Hash; \
    if ($actualHash -ne $expectedHash) { exit 1 }; \
    Expand-Archive -Path aspnet-app.zip -DestinationPath /app -Force

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

ENV DIAMOL_KEY_1="first value" \
    DIAMOL_KEY_2="second value"

WORKDIR /inetpub/wwwroot
COPY --from=installer /app/ ./