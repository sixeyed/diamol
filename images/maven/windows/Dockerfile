# escape=`
ARG WINDOWS_VERSION
ARG OPENJDK_DISTRO
ARG OPENJDK_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} as installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG MAVEN_VERSION
RUN Write-Host "Downloading Maven version: $env:MAVEN_VERSION"; `
    curl.exe -sSL -o maven.zip "https://dlcdn.apache.org/maven/maven-3/$($env:MAVEN_VERSION)/binaries/apache-maven-$($env:MAVEN_VERSION)-bin.zip"; `
    Expand-Archive maven.zip -DestinationPath C:\ ; `
    Rename-Item -Path "C:\apache-maven-$($env:MAVEN_VERSION)" -NewName C:\maven

# Maven
ARG OPENJDK_DISTRO
ARG OPENJDK_VERSION
FROM ${OPENJDK_DISTRO}:${OPENJDK_VERSION}-jdk-nanoserver

ENV MAVEN_HOME="C:\maven"

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;%MAVEN_HOME%;"

COPY --from=installer /maven ${MAVEN_HOME}