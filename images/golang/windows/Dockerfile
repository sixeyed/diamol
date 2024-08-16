#escape=`
ARG WINDOWS_VERSION
ARG GOLANG_VERSION

# Git
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION}  as git-installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG GIT_VERSION
ARG GIT_WINDOWS_RELEASE_NUMBER
ARG GIT_WINDOWS_DOWNLOAD_SHA256

RUN Write-Host "Downloading Git version: $($env:GIT_VERSION), release: $($env:GIT_WINDOWS_RELEASE_NUMBER)"; `	
    Invoke-WebRequest -OutFile git.zip -Uri "https://github.com/git-for-windows/git/releases/download/v$($env:GIT_VERSION).windows.$($env:GIT_WINDOWS_RELEASE_NUMBER)/MinGit-$($env:GIT_VERSION)-64-bit.zip"

RUN if ((Get-FileHash git.zip -Algorithm sha256).Hash -ne $env:GIT_WINDOWS_DOWNLOAD_SHA256) {exit 1}; `
    Expand-Archive -Path git.zip -DestinationPath C:\git; `
    Remove-Item git.zip -Force

# Golang
ARG GOLANG_VERSION
FROM golang:${GOLANG_VERSION}-nanoserver
ENV CGO_ENABLED=0

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;"

COPY --from=git-installer /git/ /git

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/