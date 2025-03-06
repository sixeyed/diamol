#escape=`
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/servercore:${WINDOWS_VERSION} AS servercore
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]


FROM servercore AS vcredist
RUN Invoke-WebRequest -OutFile VC_redist.x64.exe 'https://aka.ms/vs/17/release/VC_redist.x64.exe'
RUN Start-Process .\VC_redist.x64.exe -ArgumentList '/install /passive /norestart' -Wait

FROM servercore AS git
ARG 7ZIP_VERSION=2409

RUN curl.exe -sSL "http://7-zip.org/a/7z$($env:7ZIP_VERSION)-x64.msi" -o '7z.msi'; `
    Start-Process msiexec.exe -ArgumentList '/i', '7z.msi', '/quiet', '/norestart' -NoNewWindow -Wait; `
    Remove-Item 7z.msi

ARG GIT_WINDOWS_VERSION
ARG GIT_WINDOWS_RELEASE_NUMBER
ARG GIT_WINDOWS_DOWNLOAD_SHA256

RUN Write-Host "Downloading Git version: $($env:GIT_WINDOWS_VERSION), release: $($env:GIT_WINDOWS_RELEASE_NUMBER)"; `	
    curl.exe -o git.7z.exe -sSL "https://github.com/git-for-windows/git/releases/download/v$($env:GIT_WINDOWS_VERSION).windows.$($env:GIT_WINDOWS_RELEASE_NUMBER)/PortableGit-$($env:GIT_WINDOWS_VERSION)-64-bit.7z.exe"

RUN Write-Host 'Expanding Git...'; `
    if ((Get-FileHash git.7z.exe -Algorithm sha256).Hash.ToLower() -ne $env:GIT_WINDOWS_DOWNLOAD_SHA256) {exit 1}; `
    & 'C:\Program Files\7-Zip\7z.exe' x -oC:\git .\git.7z.exe


# windows-deps
ARG WINDOWS_VERSION
FROM mcr.microsoft.com/windows/nanoserver:${WINDOWS_VERSION}

COPY --from=git /git /git
COPY --from=servercore /Windows/System32/nslookup.exe /nslookup.exe
COPY --from=vcredist C:\windows\system32\msvcp140.dll C:\windows\system32
COPY --from=vcredist C:\windows\system32\vcruntime140.dll C:\windows\system32
