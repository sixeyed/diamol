ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG SQLPACKAGE_DOWNLOAD_URL
RUN Invoke-WebRequest -Uri $env:SQLPACKAGE_DOWNLOAD_URL -OutFile 'sqlpackage.zip'; \
    Expand-Archive -Path sqlpackage.zip -DestinationPath /out/sqlpackage -Force


FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION}
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG MSSQL_DOWNLOAD_URL
ARG MSSQL_VERSION_CODE
ARG MSSQL_INSTANCE_NAME

ARG MSSQL_SA_PASSWORD=sql!!s3rv3r
COPY install.ps1 /
RUN /install.ps1

COPY install-psmodule.ps1 /
RUN /install-psmodule.ps1

COPY --from=installer /out/sqlpackage /sqlpackage

EXPOSE 1443
EXPOSE 1434

ENV ACCEPT_EULA=Y \
    MSSQL_INSTANCE_NAME=${MSSQL_INSTANCE_NAME}\
    MSSQL_SA_PASSWORD=${MSSQL_SA_PASSWORD}\
    SQLPACKAGE_HOME=/sqlpackage

COPY start.ps1 /
CMD powershell /start.ps1
