FROM diamol/base:2e AS logmonitor
ARG LOGMONITOR_VERSION="v1.1"
ADD https://github.com/microsoft/windows-container-tools/releases/download/${LOGMONITOR_VERSION}/LogMonitor.exe .

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY --from=logmonitor /LogMonitor.exe /LogMonitor.exe
COPY LogMonitorConfig.json /LogMonitor/

COPY SignUp-1.0.msi /
RUN Start-Process msiexec.exe -ArgumentList '/i', 'C:\SignUp-1.0.msi', '/quiet', '/norestart' -NoNewWindow -Wait

WORKDIR /
ENTRYPOINT ./LogMonitor.exe ServiceMonitor.exe w3svc