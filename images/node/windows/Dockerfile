# escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS installer
ARG NODE_VERSION
USER ContainerAdministrator
RUN echo "Downloading Node version: %NODE_VERSION%" && `
    curl -sSL -o node.zip "https://nodejs.org/dist/v%NODE_VERSION%/node-v%NODE_VERSION%-win-x64.zip"
WORKDIR /nodejs
RUN tar -xf /node.zip --strip-components 1

# NodeJS
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

ENV NPM_CONFIG_LOGLEVEL info
CMD [ "node.exe" ]

COPY --from=installer /nodejs /nodejs

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\nodejs"

# see a more complete version - with Git & Yarn - from Stefan Scherer
# https://github.com/StefanScherer/dockerfiles-windows/blob/master/node/10/nano/Dockerfile