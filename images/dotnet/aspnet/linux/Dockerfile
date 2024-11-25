ARG DOTNET_VERSION
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine

RUN apk --no-cache add curl

EXPOSE 8080
ENV SQLITE_DATA_DIRECTORY="/data" \
    SQLITE_USER='root'