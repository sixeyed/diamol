FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/ToDoList.csproj .
RUN dotnet restore

COPY src/ .
RUN dotnet publish -c Release -o /out ToDoList.csproj

# app image
FROM diamol/dotnet-runtime

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.dll"]

ARG DATA_DIRECTORY="/data"
VOLUME $DATA_DIRECTORY

COPY --from=builder /out/ .