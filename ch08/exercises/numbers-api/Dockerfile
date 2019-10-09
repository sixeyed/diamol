FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/Numbers.Api.csproj .
RUN dotnet restore

COPY src/ .
RUN dotnet publish -c Release -o /out Numbers.Api.csproj

# app image
FROM diamol/dotnet-aspnet

ENTRYPOINT ["dotnet", "/app/Numbers.Api.dll"]
HEALTHCHECK CMD curl --fail http://localhost/rng

WORKDIR /app
COPY --from=builder /out/ .