ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION}
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG MSSQL_DOWNLOAD_URL
ARG MSSQL_VERSION_CODE
ARG MSSQL_INSTANCE_NAME

ARG MSSQL_SA_PASSWORD=sql!!s3rv3r
COPY install.ps1 /
RUN /install.ps1

EXPOSE 1443
EXPOSE 1434

ENV ACCEPT_EULA=Y \
    MSSQL_INSTANCE_NAME=${MSSQL_INSTANCE_NAME}\
    MSSQL_SA_PASSWORD=${MSSQL_SA_PASSWORD}

COPY start.ps1 /
CMD powershell /start.ps1
