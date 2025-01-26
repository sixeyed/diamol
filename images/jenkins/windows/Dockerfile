# escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG JENKINS_VERSION

WORKDIR /docker
RUN curl.exe -o docker.zip -sSL https://download.docker.com/win/static/stable/x86_64/docker-27.4.1.zip; `
    Expand-Archive -Path docker.zip -DestinationPath / -Force

WORKDIR /jenkins
RUN Write-Host "Downloading Jenkins version: $env:JENKINS_VERSION"; `
    curl.exe -o jenkins.war -sSL "https://get.jenkins.io/war-stable/$($env:JENKINS_VERSION)/jenkins.war"

# Git
ARG WINDOWS_VERSION
FROM diamol/git-windows:2e-windows-${WINDOWS_VERSION}-amd64 AS git

# Jenkins
FROM diamol/openjdk:2e-windows-${WINDOWS_VERSION}-amd64

ARG JENKINS_VERSION
ENV JENKINS_VERSION=${JENKINS_VERSION} `
    JENKINS_HOME="C:\data"

COPY --from=git /git/ /git
COPY --from=installer /docker/ /docker
COPY --from=installer /jenkins/ /jenkins

EXPOSE 8080 
ENTRYPOINT java -Duser.home=${JENKINS_HOME} -Djenkins.install.runSetupWizard=false -jar /jenkins/jenkins.war

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;C:\docker;"

COPY ./jenkins.install.UpgradeWizard.state ${JENKINS_HOME}/
COPY ./scripts/ ${JENKINS_HOME}/init.groovy.d/