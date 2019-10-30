# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG GRAFANA_VERSION="6.4.3"
ARG GRAFANA_SHA256="887ed9f4b1e650a3ff0332dc5ca79c6e55dedebcfca820bbe46590cbbeaa71f3"

RUN Write-Host "Downloading Grafana version: $env:GRAFANA_VERSION"; `
    Invoke-WebRequest "https://dl.grafana.com/oss/release/grafana-$($env:GRAFANA_VERSION).windows-amd64.zip"  -OutFile grafana.zip -UseBasicParsing; `
    if ((Get-FileHash grafana.zip -Algorithm sha256).Hash.ToLower() -ne $env:GRAFANA_SHA256) {exit 1}

RUN Expand-Archive grafana.zip -DestinationPath C:\; `
    Move-Item "grafana-$($env:GRAFANA_VERSION)" grafana

# Grafana
FROM mcr.microsoft.com/windows/nanoserver:1809

EXPOSE 3000

WORKDIR C:\grafana\bin
CMD ["grafana-server.exe"]

ENV GF_PATHS_CONFIG="C:\\grafana\\conf\\defaults.ini" `
    GF_PATHS_DATA="C:\\grafana\\data" `
    GF_PATHS_HOME="C:\\grafana" `
    GF_PATHS_LOGS="C:\\grafana\\data\\log" `
    GF_PATHS_PLUGINS="C:\\grafana\\data\\plugins" `
    GF_PATHS_PROVISIONING="C:\\grafana\\conf\\provisioning"

COPY --from=installer C:\grafana C:\grafana