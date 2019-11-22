####
#### Download and prepare PostgreSQL for Windows
####
FROM mcr.microsoft.com/windows/servercore:1809 as prepare
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Install correct Visual C++ Redistributable Package
RUN Write-Host('Visual C++ 2017 Redistributable Package') ; \
    $URL2 = 'https://download.visualstudio.microsoft.com/download/pr/11100230/15ccb3f02745c7b206ad10373cbca89b/VC_redist.x64.exe' ; \
    Invoke-WebRequest -Uri $URL2 -OutFile 'C:\\vcredist.exe' ; \
    Start-Process 'C:\\vcredist.exe' -Wait \
    -ArgumentList @( \
    '/install', \
    '/passive', \
    '/norestart' \
    )

# Set the variables for EnterpriseDB
ARG EDB_VER=11.5-1
ENV EDB_VER $EDB_VER
ENV EDB_REPO https://get.enterprisedb.com/postgresql

##### Use PowerShell for the installation
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

### Download EnterpriseDB and remove cruft
RUN $URL1 = $('{0}/postgresql-{1}-windows-x64-binaries.zip' -f $env:EDB_REPO,$env:EDB_VER) ; \
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
FROM mcr.microsoft.com/windows/nanoserver:1809

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