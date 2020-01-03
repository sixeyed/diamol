FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/core/ToDoList.Core.csproj ./core/
COPY src/entities/ToDoList.Entities.csproj ./entities/
COPY src/messaging/ToDoList.Messaging.csproj ./messaging/
COPY src/model/ToDoList.Model.csproj ./model/
COPY src/mutating-handler/ToDoList.MutatingHandler.csproj ./mutating-handler/

WORKDIR /src/mutating-handler
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out ToDoList.MutatingHandler.csproj

# app image
FROM diamol/dotnet-runtime

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.MutatingHandler.dll"]
ENV MessageQueue__Url="nats://TODO"

COPY --from=builder /out/ .