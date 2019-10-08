FROM mcr.microsoft.com/windows/nanoserver:1809

USER ContainerAdministrator

# copy utils from Server Core
COPY --from=mcr.microsoft.com/windows/servercore:ltsc2019 /Windows/System32/nslookup.exe /Windows/System32/nslookup.exe 

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/
