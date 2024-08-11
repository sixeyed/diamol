ARG WINDOWS_NANOSERVER_VERSION
ARG WINDOWS_SERVERCORE_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_NANOSERVER_VERSION}

USER ContainerAdministrator

# copy utils from Server Core
COPY --from=mcr.microsoft.com/windows/servercore:${WINDOWS_SERVERCORE_VERSION} /Windows/System32/nslookup.exe /Windows/System32/nslookup.exe 

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/
