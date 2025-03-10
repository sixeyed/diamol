FROM diamol/dotnet-sdk:2e as builder

WORKDIR /src
COPY src/whoami.csproj .
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out whoami.csproj

# app image
FROM  diamol/dotnet-aspnet:2e

EXPOSE 5000

WORKDIR /app
ENTRYPOINT ["dotnet", "whoami.dll"]

COPY --from=builder /out/ .