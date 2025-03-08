FROM diamol/dotnet-sdk:2e AS builder

WORKDIR /src
COPY src/TimeCheck.csproj .
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out TimeCheck.csproj

# app image
FROM diamol/dotnet-runtime:2e

ENV Application__Version="1.0" \
    Output__Console="true"

WORKDIR /app
ENTRYPOINT ["dotnet", "TimeCheck.dll"]

COPY --from=builder /out/ .