# escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} as installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG NODE_VERSION
RUN Write-Host "Downloading Node version: $env:NODE_VERSION"; `
    Invoke-WebRequest -OutFile node.zip "https://nodejs.org/dist/v$($env:NODE_VERSION)/node-v$($env:NODE_VERSION)-win-x64.zip"; `
    Expand-Archive node.zip -DestinationPath C:\ ; `
    Rename-Item -Path "C:\node-v$($env:NODE_VERSION)-win-x64" -NewName C:\nodejs

# NodeJS
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

ENV NPM_CONFIG_LOGLEVEL info
CMD [ "node.exe" ]

COPY --from=installer /nodejs /nodejs

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\nodejs"
USER ContainerUser

# see a more complete version - with Git & Yarn - from Stefan Scherer
# https://github.com/StefanScherer/dockerfiles-windows/blob/master/node/10/nano/Dockerfile