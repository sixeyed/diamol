FROM diamol/nslookup:2e AS nslookup

USER ContainerAdministrator

# copy utils from Server Core
COPY --from=nslookup /nslookup.exe /Windows/System32/nslookup.exe 

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/
