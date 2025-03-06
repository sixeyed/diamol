ARG WINDOWS_VERSION
FROM diamol/windows-deps:2e-windows-${WINDOWS_VERSION}-amd64 AS windows-deps

ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

USER ContainerAdministrator

# copy utils from Server Core
COPY --from=windows-deps /nslookup.exe /Windows/System32/nslookup.exe 

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/
