# escape=`
ARG WINDOWS_VERSION

FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION} AS installer
ARG TRIVY_VERSION
USER ContainerAdministrator
RUN echo "Downloading Trivy version: %TRIVY_VERSION%" && `
    curl -sSL -o trivy.zip "https://github.com/aquasecurity/trivy/releases/download/v%TRIVY_VERSION%/trivy_%TRIVY_VERSION%_windows-64bit.zip"
WORKDIR /trivy
RUN tar -xf /trivy.zip

# Trivy
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

CMD [ "trivy.exe" ]
COPY --from=installer /trivy /trivy

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\trivy"