FROM diamol/dotnet-sdk:2e AS build

WORKDIR /src
COPY src/NerdDinner.Database/ ./NerdDinner.Database
COPY src/NerdDinner.Database.Build/ ./NerdDinner.Database.Build

WORKDIR /src/NerdDinner.Database.Build
RUN dotnet build NerdDinner.Database.Build.csproj -o /out


FROM diamol/mssql:2e
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop';"]

WORKDIR /init
COPY --from=build /out/NerdDinner.Database.Build.dacpac ./NerdDinner.dacpac
COPY docker/db/initialize.ps1 ./

VOLUME C:/data
CMD ./initialize.ps1 -sa_password $env:MSSQL_SA_PASSWORD -Verbose

ENV MSSQL_SA_PASSWORD=N3rdDinner!!