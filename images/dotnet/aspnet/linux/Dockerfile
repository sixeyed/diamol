ARG DOTNET_VERSION
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine
EXPOSE 80
ENV SQLITE_DATA_DIRECTORY="/data" \
    SQLITE_USER='root'