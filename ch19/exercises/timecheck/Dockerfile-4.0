FROM diamol/dotnet-sdk as builder

WORKDIR /src
COPY src/TimeCheck.csproj .

COPY src/ .
RUN dotnet restore

COPY src /src
RUN dotnet publish -c Release -o /out TimeCheck.csproj

# app image
FROM diamol/dotnet-runtime

ARG BUILD_NUMBER=0
ARG BUILD_TAG=local

LABEL version="4.0"
LABEL build_number=${BUILD_NUMBER}
LABEL build_tag=${BUILD_TAG}

ENV Application__Version="4.0"

WORKDIR /app
ENTRYPOINT ["dotnet", "TimeCheck.dll"]

COPY --from=builder /out/ .