FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/ToDoList.csproj .
RUN dotnet restore

COPY src/ .
RUN dotnet publish -c Release -o /out ToDoList.csproj

# app image
FROM diamol/dotnet-aspnet

ARG BUILD_NUMBER=0
LABEL build_number=${BUILD_NUMBER}

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.dll"]

COPY --from=builder /out/ .