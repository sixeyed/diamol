FROM diamol/dotnet-sdk as builder
WORKDIR /src
COPY src/TimeCheck.csproj .
RUN dotnet restore
COPY src /src
RUN dotnet publish -c Release -o /out TimeCheck.csproj

# tail utility
FROM diamol/dotnet-sdk as utility
WORKDIR /utility
COPY utility/Tail.csproj .
RUN dotnet restore
COPY utility /utility
RUN dotnet publish -c Release -o /out Tail.csproj

# app image
FROM diamol/dotnet-runtime AS base

ARG BUILD_NUMBER=0
ARG BUILD_TAG=local

LABEL version="5.0"
LABEL build_number=${BUILD_NUMBER}
LABEL build_tag=${BUILD_TAG}

ENV Application__Version="5.0" \
    DOTNET_USE_POLLING_FILE_WATCHER="true"

WORKDIR /logs
RUN echo Init > timecheck.log

WORKDIR /app
COPY --from=builder /out/ .
COPY --from=utility /out/ .

# windows
FROM base AS windows
CMD start /B dotnet TimeCheck.dll && dotnet Tail.dll /logs timecheck.log

# linux
FROM base AS linux
CMD dotnet TimeCheck.dll & dotnet Tail.dll /logs timecheck.log