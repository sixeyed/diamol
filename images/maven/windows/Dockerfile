# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 as installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG MAVEN_VERSION="3.6.3"

RUN Write-Host "Downloading Maven version: $env:MAVEN_VERSION"; `
    Invoke-WebRequest -OutFile maven.zip "http://apache.mirror.anlx.net/maven/maven-3/$($env:MAVEN_VERSION)/binaries/apache-maven-$($env:MAVEN_VERSION)-bin.zip"; `
    Expand-Archive maven.zip -DestinationPath C:\ ; `
    Rename-Item -Path "C:\apache-maven-$($env:MAVEN_VERSION)" -NewName C:\maven

# Maven
FROM openjdk:11-jdk
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; "]

ENV MAVEN_HOME="C:\maven"

RUN $env:PATH = '{0}\bin;{1}' -f $env:MAVEN_HOME, $env:PATH ; `
	[Environment]::SetEnvironmentVariable('PATH', $env:PATH, [EnvironmentVariableTarget]::Machine)

COPY --from=installer /maven ${MAVEN_HOME}