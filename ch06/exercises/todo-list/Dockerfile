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

# for Windows use --build-arg DATA_DIRECTORY=C:\data
ARG DATA_DIRECTORY="/data"
VOLUME $DATA_DIRECTORY

 # for Windows use --build-arg USER=ContainerAdministrator
ARG USER='root'
USER $USER

COPY --from=builder /out/ .