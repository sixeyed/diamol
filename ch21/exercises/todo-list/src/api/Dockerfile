FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/core/ToDoList.Core.csproj ./core/
COPY src/entities/ToDoList.Entities.csproj ./entities/
COPY src/messaging/ToDoList.Messaging.csproj ./messaging/
COPY src/api/ToDoList.Api.csproj ./api/

WORKDIR /src/api
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out ToDoList.Api.csproj

# app image
FROM diamol/dotnet-aspnet

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.Api.dll"]

COPY --from=builder /out/ .