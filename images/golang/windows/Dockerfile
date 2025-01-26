#escape=`

# Git
ARG WINDOWS_VERSION
ARG GOLANG_VERSION
FROM diamol/git-windows:2e-windows-${WINDOWS_VERSION}-amd64 AS git

# Golang
ARG GOLANG_VERSION
FROM golang:${GOLANG_VERSION}-nanoserver
ENV CGO_ENABLED=0

USER ContainerAdministrator
RUN setx /M PATH "%PATH%;C:\git\cmd;C:\git\mingw64\bin;C:\git\usr\bin;"

COPY --from=git /git/ /git

# create commands which alias Linux names
COPY ./aliases/ /Windows/System32/