FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/ToDoList.csproj .
RUN dotnet restore

COPY src/ .
RUN dotnet publish -c Release -o /out ToDoList.csproj

# app image
FROM diamol/dotnet-aspnet

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.dll"]

# set in the base image - `/data` for Linux, `C:\data` for Windows
VOLUME $SQLITE_DATA_DIRECTORY

# set in the base image - `root` for Linux, `ContainerAdministrator` for Windows
USER $SQLITE_USER

COPY --from=builder /out/ .