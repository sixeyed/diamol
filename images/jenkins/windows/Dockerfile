# escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG JENKINS_VERSION="2.263.4"

WORKDIR /docker
RUN [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest "https://github.com/docker/compose/releases/download/1.28.5/docker-compose-Windows-x86_64.exe" -UseBasicParsing -OutFile docker-compose.exe; `
    Invoke-WebRequest "https://github.com/StefanScherer/docker-cli-builder/releases/download/19.03.12/docker.exe" -UseBasicParsing -OutFile docker.exe

WORKDIR /jenkins
RUN Write-Host "Downloading Jenkins version: $env:JENKINS_VERSION"; `
    Invoke-WebRequest "http://mirrors.jenkins.io/war-stable/$($env:JENKINS_VERSION)/jenkins.war.sha256" -OutFile 'jenkins.war.sha256' -UseBasicParsing; `    
    Invoke-WebRequest "http://mirrors.jenkins.io/war-stable/$($env:JENKINS_VERSION)/jenkins.war" -OutFile 'jenkins.war' -UseBasicParsing

RUN $env:JENKINS_SHA256=$(Get-Content -Raw jenkins.war.sha256).Split(' ')[0]; `
    if ((Get-FileHash jenkins.war -Algorithm sha256).Hash.ToLower() -ne $env:JENKINS_SHA256) {exit 1}

# Jenkins
FROM openjdk:8-nanoserver-1809

ARG JENKINS_VERSION="2.263.4"
ENV JENKINS_VERSION=${JENKINS_VERSION} `
    JENKINS_HOME="C:\data"

COPY --from=diamol/git-windows /git/ /git
COPY --from=installer /docker/ /docker
COPY --from=installer /jenkins/ /jenkins

EXPOSE 8080 
ENTRYPOINT java -Duser.home=${JENKINS_HOME} -Djenkins.install.runSetupWizard=false -jar /jenkins/jenkins.war

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;C:\docker;"

COPY ./jenkins.install.UpgradeWizard.state ${JENKINS_HOME}/
COPY ./scripts/ ${JENKINS_HOME}/init.groovy.d/