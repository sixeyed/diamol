FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/core/ToDoList.Core.csproj ./core/
COPY src/entities/ToDoList.Entities.csproj ./entities/
COPY src/messaging/ToDoList.Messaging.csproj ./messaging/
COPY src/model/ToDoList.Model.csproj ./model/
COPY src/web/ToDoList.csproj ./web/

WORKDIR /src/web
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out ToDoList.csproj

# app image
FROM diamol/dotnet-aspnet

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.dll"]

COPY --from=builder /out/ .