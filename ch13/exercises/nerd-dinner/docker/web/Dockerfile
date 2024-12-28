FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build

WORKDIR /src
COPY src/NerdDinner.sln .
COPY src/NerdDinner/NerdDinner.csproj ./NerdDinner/NerdDinner.csproj
COPY src/NerdDinner/packages.config ./NerdDinner/packages.config
RUN nuget restore

COPY src/ .
RUN msbuild NerdDinner/NerdDinner.csproj /p:DeployOnBuild=true 


FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8
WORKDIR /inetpub/wwwroot
COPY --from=build /src/NerdDinner/obj/Debug/Package/PackageTmp/ ./
COPY docker/web/override/ ./
COPY docker/web/startup.ps1 /

ENTRYPOINT ["powershell"]
CMD ["-File", "/startup.ps1"]
 