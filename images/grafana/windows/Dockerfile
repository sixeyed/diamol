# escape=`
ARG WINDOWS_VERSION
FROM diamol/windows-deps:2e-windows-${WINDOWS_VERSION}-amd64 AS windows-deps

ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS installer
ARG GRAFANA_VERSION
USER ContainerAdministrator
RUN echo "Downloading Grafana version: %GRAFANA_VERSION%" && `
    curl -sSL -o grafana.zip "https://dl.grafana.com/oss/release/grafana-%GRAFANA_VERSION%.windows-amd64.zip"
WORKDIR /grafana
RUN tar -xf /grafana.zip --strip-components 1

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
COPY --from=windows-deps /netapi32.dll  /windows/system32/netapi32.dll