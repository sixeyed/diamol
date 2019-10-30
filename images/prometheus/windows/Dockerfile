# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG PROMETHEUS_VERSION="2.13.1"
ARG PROMETHEUS_SHA256="44dd2834f753a527131da30a133de3ba35152356525a8ab7a24753770c593bfa"

RUN Write-Host "Downloading Prometheus version: $env:PROMETHEUS_VERSION"; `
    Invoke-WebRequest "https://github.com/prometheus/prometheus/releases/download/v$($env:PROMETHEUS_VERSION)/prometheus-$($env:PROMETHEUS_VERSION).windows-amd64.tar.gz" -OutFile 'prometheus.tar.gz' -UseBasicParsing; `
    if ((Get-FileHash prometheus.tar.gz -Algorithm sha256).Hash.ToLower() -ne $env:PROMETHEUS_SHA256) {exit 1}

RUN tar xf prometheus.tar.gz; `
    Rename-Item -Path "C:\prometheus-$($env:PROMETHEUS_VERSION).windows-amd64" -NewName 'C:\prometheus'

# Prometheus
FROM mcr.microsoft.com/windows/nanoserver:1809

COPY --from=installer /windows/system32/netapi32.dll /windows/system32/netapi32.dll
COPY --from=installer /prometheus/prometheus.exe      /bin/prometheus.exe
COPY --from=installer /prometheus/promtool.exe        /bin/promtool.exe
COPY --from=installer /prometheus/prometheus.yml      /etc/prometheus/prometheus.yml
COPY --from=installer /prometheus/console_libraries/  /etc/prometheus/
COPY --from=installer /prometheus/consoles/           /etc/prometheus/

EXPOSE     9090
VOLUME     C:\prometheus
USER ContainerAdministrator

# Prometheus needs /etc/resolv.conf to lookup DNS targets
CMD echo %DOCKER_HOST%  DOCKER_HOST >> /windows/system32/drivers/etc/hosts && `
    C:\\set-gateway.cmd && `
    C:\\bin\\prometheus.exe `
    --storage.tsdb.path=/prometheus `
    --web.console.libraries=/etc/prometheus/console_libraries `
    --web.console.templates=/etc/prometheus/consoles `
    --config.file=/etc/prometheus/prometheus.yml

COPY set-gateway.cmd /
COPY prometheus.yml /etc/prometheus/prometheus.yml