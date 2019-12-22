FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/Pi.Web.csproj .
RUN dotnet restore

COPY src/ .
RUN dotnet publish -c Release -o /out Pi.Web.csproj

# pi
FROM diamol/dotnet-aspnet

EXPOSE 80
ENTRYPOINT ["dotnet", "Pi.Web.dll"]

WORKDIR /app
COPY --from=builder /out/ .