ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS servercore

ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

USER ContainerAdministrator

# copy utils from Server Core
COPY --from=servercore /Windows/System32/nslookup.exe /Windows/System32/nslookup.exe 

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/
