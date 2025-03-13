# escape=`
ARG WINDOWS_VERSION
ARG OPENJDK_DISTRO
ARG OPENJDK_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} as installer
ARG MAVEN_VERSION
USER ContainerAdministrator
RUN echo "Downloading Maven version: %MAVEN_VERSION%" && `
    curl.exe -sSL -o maven.zip "https://dlcdn.apache.org/maven/maven-3/%MAVEN_VERSION%/binaries/apache-maven-%MAVEN_VERSION%-bin.zip"
WORKDIR /maven
RUN tar -xf /maven.zip --strip-components 1

# Maven
ARG OPENJDK_DISTRO
ARG OPENJDK_VERSION
FROM ${OPENJDK_DISTRO}:${OPENJDK_VERSION}-jdk-nanoserver

ENV MAVEN_HOME="C:\maven" `
    MAVEN_BIN="C:\maven\bin"

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;%MAVEN_HOME%;%MAVEN_BIN%;"

COPY --from=installer /maven ${MAVEN_HOME}