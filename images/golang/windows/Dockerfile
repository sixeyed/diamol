#escape=`

# Git
FROM mcr.microsoft.com/windows/servercore:ltsc2019 as git-installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG GIT_VERSION="2.24.0"
ARG GIT_RELEASE_NUMBER="2"
ARG GIT_DOWNLOAD_SHA256="c33aec6ae68989103653ca9fb64f12cabccf6c61d0dde30c50da47fc15cf66e2"

RUN Write-Host "Downloading Git version: $($env:GIT_VERSION), release: $($env:GIT_RELEASE_NUMBER)"; `	
    Invoke-WebRequest -OutFile git.zip -Uri "https://github.com/git-for-windows/git/releases/download/v$($env:GIT_VERSION).windows.$($env:GIT_RELEASE_NUMBER)/MinGit-$($env:GIT_VERSION).$($env:GIT_RELEASE_NUMBER)-64-bit.zip"

RUN if ((Get-FileHash git.zip -Algorithm sha256).Hash -ne $env:GIT_DOWNLOAD_SHA256) {exit 1}; `
    Expand-Archive -Path git.zip -DestinationPath C:\git; `
    Remove-Item git.zip -Force

# Golang
FROM golang:1.13-nanoserver-1809
ENV CGO_ENABLED=0

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;"

COPY --from=git-installer /git/ /git

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/