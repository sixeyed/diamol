# escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG GRAFANA_VERSION
ARG GRAFANA_WINDOWS_DOWNLOAD_SHA256

RUN Write-Host "Downloading Grafana version: $env:GRAFANA_VERSION"; `
    Invoke-WebRequest "https://dl.grafana.com/oss/release/grafana-$($env:GRAFANA_VERSION).windows-amd64.zip"  -OutFile grafana.zip -UseBasicParsing; `
    if ((Get-FileHash grafana.zip -Algorithm sha256).Hash.ToLower() -ne $env:GRAFANA_WINDOWS_DOWNLOAD_SHA256) {exit 1}

RUN Expand-Archive grafana.zip -DestinationPath C:\; `
    Move-Item "grafana-v$($env:GRAFANA_VERSION)" grafana

# Grafana
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

EXPOSE 3000

WORKDIR C:\grafana\bin
CMD ["grafana-server.exe"]

ENV GF_PATHS_CONFIG="C:\\grafana\\conf\\defaults.ini" `
    GF_PATHS_DATA="C:\\grafana\\data" `
    GF_PATHS_HOME="C:\\grafana" `
    GF_PATHS_LOGS="C:\\grafana\\data\\log" `
    GF_PATHS_PLUGINS="C:\\grafana\\data\\plugins" `
    GF_PATHS_PROVISIONING="C:\\grafana\\conf\\provisioning"

ENV GF_AUTH_ANONYMOUS_ENABLED=true `
    GF_AUTH_ANONYMOUS_ORG_ROLE=Admin `
    GF_AUTH_DISABLE_LOGIN_FORM=true `
    GF_USERS_DEFAULT_THEME=light

COPY --from=installer C:\grafana C:\grafana