FROM diamol/dotnet-sdk:2e AS builder

WORKDIR /src
COPY src/Pi.Math/Pi.Math.csproj ./Pi.Math/
COPY src/Pi.Runtime/Pi.Runtime.csproj ./Pi.Runtime/
COPY src/Pi.Web/Pi.Web.csproj ./Pi.Web/

WORKDIR /src/Pi.Web
RUN dotnet restore

COPY src/Pi.Math/ /src/Pi.Math/
COPY src/Pi.Runtime /src/Pi.Runtime/
COPY src/Pi.Web /src/Pi.Web/
RUN dotnet publish -c Release -o /out Pi.Web.csproj

# app image
FROM diamol/dotnet-aspnet:2e 

ENV ASPNETCORE_HTTP_PORTS=80 \
    Computation__Default__DecimalPlaces=80000
    
EXPOSE 80
ENTRYPOINT ["dotnet", "Pi.Web.dll"]
CMD ["-m", "web"]

WORKDIR /app
COPY --from=builder /out/ .