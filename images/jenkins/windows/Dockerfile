# escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS installer
USER ContainerAdministrator

ARG JENKINS_VERSION
ARG DOCKER_VERSION=27.4.1

RUN echo "Downloading Docker version: %DOCKER_VERSION%" && `
    curl.exe -o docker.zip -sSL https://download.docker.com/win/static/stable/x86_64/docker-%DOCKER_VERSION%.zip
WORKDIR /docker
RUN tar -xf /docker.zip --strip-components 1

WORKDIR /jenkins
RUN echo "Downloading Jenkins version: %JENKINS_VERSION%" && `
    curl.exe -o jenkins.war -sSL "https://get.jenkins.io/war-stable/%JENKINS_VERSION%/jenkins.war"

# Git
ARG WINDOWS_VERSION
FROM diamol/windows-deps:2e-windows-${WINDOWS_VERSION}-amd64 AS git

# Jenkins
FROM diamol/openjdk:2e-windows-${WINDOWS_VERSION}-amd64

ARG JENKINS_VERSION
ENV JENKINS_VERSION=${JENKINS_VERSION} `
    JENKINS_HOME="C:\data"

COPY --from=git /git/ /git
COPY --from=installer /docker/docker.exe /docker
COPY --from=installer /jenkins/ /jenkins

EXPOSE 8080 
ENTRYPOINT java -Duser.home=${JENKINS_HOME} -Djenkins.install.runSetupWizard=false -jar /jenkins/jenkins.war --enable-future-java

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;C:\docker;"

COPY ./jenkins.install.UpgradeWizard.state ${JENKINS_HOME}/
COPY ./scripts/ ${JENKINS_HOME}/init.groovy.d/