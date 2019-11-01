FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/Numbers.Web/Numbers.Web.csproj .
RUN dotnet restore

COPY src/Numbers.Web/ .
RUN dotnet publish -c Release -o /out Numbers.Web.csproj

# http check utility
FROM diamol/dotnet-sdk AS http-check-builder

WORKDIR /src
COPY src/Utilities.HttpCheck/Utilities.HttpCheck.csproj .
RUN dotnet restore

COPY src/Utilities.HttpCheck/ .
RUN dotnet publish -c Release -o /out Utilities.HttpCheck.csproj

# app image
FROM diamol/dotnet-aspnet

ENV RngApi__Url=http://numbers-api/rng

CMD dotnet Utilities.HttpCheck.dll -c RngApi:Url -t 900 && \
    dotnet Numbers.Web.dll

WORKDIR /app
COPY --from=http-check-builder /out/ .
COPY --from=builder /out/ .