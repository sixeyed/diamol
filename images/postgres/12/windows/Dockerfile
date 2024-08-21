ARG WINDOWS_VERSION

####
#### Download and prepare PostgreSQL for Windows
####
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} as prepare
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# VC++ redistributables
RUN Invoke-WebRequest -OutFile C:\\VC_redist.x64.exe 'https://aka.ms/vs/17/release/VC_redist.x64.exe'
RUN Start-Process C:\\VC_redist.x64.exe -ArgumentList '/install /passive /norestart' -Wait

# downloads: https://www.enterprisedb.com/download-postgresql-binaries
ARG ENTERPRISE_DB_DOWNLOAD_FILE_ID

### Download EnterpriseDB and remove cruft
RUN $URL1 = 'https://sbp.enterprisedb.com/getfile.jsp?fileid=' + $env:ENTERPRISE_DB_DOWNLOAD_FILE_ID ; \
    echo "Downloading: $URL1" ; \
    Invoke-WebRequest -Uri $URL1 -OutFile 'C:\\EnterpriseDB.zip' ; \
    Expand-Archive 'C:\\EnterpriseDB.zip' -DestinationPath 'C:\\' ; \
    Remove-Item -Path 'C:\\EnterpriseDB.zip' ; \
    Remove-Item -Recurse -Force –Path 'C:\\pgsql\\doc' ; \
    Remove-Item -Recurse -Force –Path 'C:\\pgsql\\include' ; \
    Remove-Item -Recurse -Force –Path 'C:\\pgsql\\pgAdmin*' ; \
    Remove-Item -Recurse -Force –Path 'C:\\pgsql\\StackBuilder'

### Make the sample config easier to munge (and "correct by default")
RUN $SAMPLE_FILE = 'C:\\pgsql\\share\\postgresql.conf.sample' ; \
    $SAMPLE_CONF = Get-Content $SAMPLE_FILE ; \
    $SAMPLE_CONF = $SAMPLE_CONF -Replace '#listen_addresses = ''localhost''','listen_addresses = ''*''' ; \
    $SAMPLE_CONF | Set-Content $SAMPLE_FILE

# Copy relevant DLLs to PostgreSQL
RUN Copy-Item 'C:\\windows\\system32\\vcruntime140.dll' -Destination 'C:\\pgsql\\bin\\vcruntime140.dll'

####
#### PostgreSQL on Windows Nano Server
####
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

RUN mkdir "C:\\docker-entrypoint-initdb.d"

#### Copy over PostgreSQL
COPY --from=prepare /pgsql /pgsql

#### In order to set system PATH, ContainerAdministrator must be used
USER ContainerAdministrator
RUN setx /M PATH "C:\\pgsql\\bin;%PATH%"
USER ContainerUser
ENV PGDATA "C:\\pgsql\\data"
VOLUME ${PGDATA}

COPY docker-entrypoint.cmd /
ENTRYPOINT ["C:\\docker-entrypoint.cmd"]

EXPOSE 5432
CMD ["postgres"]