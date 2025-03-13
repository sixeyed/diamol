# escape=`
ARG WINDOWS_VERSION
FROM diamol/windows-deps:2e-windows-${WINDOWS_VERSION}-amd64 AS windows-deps

FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS installer
ARG PROMETHEUS_VERSION
USER ContainerAdministrator
RUN echo "Downloading Prometheus version: %PROMETHEUS_VERSION%" && `
    curl -sSL -o prometheus.zip "https://github.com/prometheus/prometheus/releases/download/v%PROMETHEUS_VERSION%/prometheus-%PROMETHEUS_VERSION%.windows-amd64.zip"
WORKDIR /prometheus
RUN tar -xf /prometheus.zip --strip-components 1

# Prometheus
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

COPY --from=windows-deps /netapi32.dll                   /windows/system32/netapi32.dll
COPY --from=installer    /prometheus/prometheus.exe      /bin/prometheus.exe
COPY --from=installer    /prometheus/promtool.exe        /bin/promtool.exe
COPY --from=installer    /prometheus/prometheus.yml      /etc/prometheus/prometheus.yml
COPY --from=installer    /prometheus/console_libraries/  /etc/prometheus/
COPY --from=installer    /prometheus/consoles/           /etc/prometheus/

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