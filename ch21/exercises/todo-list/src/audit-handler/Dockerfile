FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/core/ToDoList.Core.csproj ./core/
COPY src/entities/ToDoList.Entities.csproj ./entities/
COPY src/messaging/ToDoList.Messaging.csproj ./messaging/
COPY src/audit-handler/ToDoList.AuditHandler.csproj ./audit-handler/

WORKDIR /src/audit-handler
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out ToDoList.AuditHandler.csproj

# app image
FROM diamol/dotnet-runtime

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.AuditHandler.dll"]

COPY --from=builder /out/ .