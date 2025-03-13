# escape=`
ARG WINDOWS_VERSION
FROM diamol/windows-deps:2e-windows-${WINDOWS_VERSION}-amd64 AS windows-deps

FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS installer
ARG ENTERPRISE_DB_DOWNLOAD_FILE_ID
USER ContainerAdministrator

RUN echo "Downloading Posgtres version: %ENTERPRISE_DB_DOWNLOAD_FILE_ID%" && `
    curl -sSL -o pgsql.zip "https://sbp.enterprisedb.com/getfile.jsp?fileid=%ENTERPRISE_DB_DOWNLOAD_FILE_ID%"
WORKDIR /pqsql
RUN tar -xf /pgsql.zip --strip-components 1
RUN rmdir /S /Q doc && `
    rmdir /S /Q include && `
    rmdir /S /Q "pgAdmin 4" && `
    rmdir /S /Q StackBuilder

# Postgres
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

RUN mkdir "C:\docker-entrypoint-initdb.d"

ARG POSTGRES_VERSION
ENV POSTGRES_VERSION ${POSTGRES_VERSION}
ENV POSTGRES_PASSWORD=postgres

#### In order to set system PATH, ContainerAdministrator must be used
USER ContainerAdministrator
RUN setx /M PATH "C:\pgsql\bin;%PATH%"
USER ContainerUser
ENV PGDATA "C:\pgsql\data"
VOLUME ${PGDATA}

COPY docker-entrypoint.cmd /
ENTRYPOINT ["C:\\docker-entrypoint.cmd"]

EXPOSE 5432
CMD ["postgres"]

COPY --from=installer /pqsql /pgsql
COPY --from=windows-deps C:\windows\system32\vcruntime140.dll C:\pgsql\bin
COPY postgresql.conf.sample C:\pgsql\share