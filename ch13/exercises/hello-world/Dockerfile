ARG WINDOWS_VERSION=ltsc2025
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS installer
SHELL ["powershell", "-Command"]
WORKDIR /install
COPY hello-world.msi .
RUN Start-Process msiexec.exe -ArgumentList '/i', 'hello-world.msi', '/quiet', '/norestart' -NoNewWindow -Wait


FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}
WORKDIR /app
COPY --from=installer ["C:/Program Files (x86)/diamol-ch13/HelloDiamol.txt", "/app/hello-world.txt"]
CMD ["cmd", "/C", "type", "hello-world.txt"]