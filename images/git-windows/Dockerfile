#escape=`
FROM mcr.microsoft.com/windows/servercore:ltsc2019 as git-installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG 7ZIP_VERSION="1604"

RUN Invoke-WebRequest "http://7-zip.org/a/7z$($env:7ZIP_VERSION)-x64.msi" -OutFile '7z.msi' -UseBasicParsing; `
    Start-Process msiexec.exe -ArgumentList '/i', '7z.msi', '/quiet', '/norestart' -NoNewWindow -Wait; `
    Remove-Item 7z.msi

ARG GIT_VERSION="2.24.0"
ARG GIT_RELEASE_NUMBER="2"
ARG GIT_DOWNLOAD_SHA256="353d0e1566d8897cb7afe2f6f9088bac17182ca43416feadec1c16f5c3bb9e0f"

RUN Write-Host "Downloading Git version: $($env:GIT_VERSION), release: $($env:GIT_RELEASE_NUMBER)"; `	
    Invoke-WebRequest -OutFile git.7z.exe -Uri "https://github.com/git-for-windows/git/releases/download/v$($env:GIT_VERSION).windows.$($env:GIT_RELEASE_NUMBER)/PortableGit-$($env:GIT_VERSION).$($env:GIT_RELEASE_NUMBER)-64-bit.7z.exe"

RUN Write-Host 'Expanding Git...'; `
    if ((Get-FileHash git.7z.exe -Algorithm sha256).Hash -ne $env:GIT_DOWNLOAD_SHA256) {exit 1}; `
    & 'C:\Program Files\7-Zip\7z.exe' x -oC:\git .\git.7z.exe
