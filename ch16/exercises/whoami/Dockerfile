FROM diamol/dotnet-sdk as builder

WORKDIR /src
COPY src/whoami.csproj .
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out whoami.csproj

# app image
FROM  diamol/dotnet-aspnet

EXPOSE 80

WORKDIR /reference-data-api
ENTRYPOINT ["dotnet", "whoami.dll"]

COPY --from=builder /out/ .