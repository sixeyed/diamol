FROM mcr.microsoft.com/windows/servercore:ltsc2019

# install chocolatey for package management
RUN Invoke-WebRequest -UseBasicParsing https://chocolatey.org/install.ps1 | Invoke-Expression

# install docker
RUN choco install -y docker
