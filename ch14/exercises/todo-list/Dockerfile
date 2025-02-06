FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /src
COPY src/ToDoList.csproj .
RUN dotnet restore

COPY src/ .
RUN dotnet publish -c Release -o /out ToDoList.csproj

# app image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
ENTRYPOINT ["dotnet", "ToDoList.dll"]

COPY --from=builder /out/ .