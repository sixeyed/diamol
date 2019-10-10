FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/Numbers.Web/Numbers.Web.csproj .
RUN dotnet restore

COPY src/Numbers.Web/ .
RUN dotnet publish -c Release -o /out Numbers.Web.csproj

# app image
FROM diamol/dotnet-aspnet

ENV RngApi:Url=http://numbers-api/rng

ENTRYPOINT ["dotnet", "/app/Numbers.Web.dll"]

WORKDIR /app
COPY --from=builder /out/ .