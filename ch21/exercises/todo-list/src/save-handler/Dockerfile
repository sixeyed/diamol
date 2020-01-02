FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/core/ToDoList.Core.csproj ./core/
COPY src/entities/ToDoList.Entities.csproj ./entities/
COPY src/messaging/ToDoList.Messaging.csproj ./messaging/
COPY src/model/ToDoList.Model.csproj ./model/
COPY src/save-handler/ToDoList.SaveHandler.csproj ./save-handler/

WORKDIR /src/save-handler
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out ToDoList.SaveHandler.csproj

# app image
FROM diamol/dotnet-runtime

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.SaveHandler.dll"]

COPY --from=builder /out/ .