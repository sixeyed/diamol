FROM diamol/dotnet-sdk AS builder

WORKDIR /src
COPY src/FileDisplay.csproj .
RUN dotnet restore

COPY src/ .
RUN dotnet publish -c Release -o /out FileDisplay.csproj

# app image
FROM diamol/dotnet-runtime

WORKDIR /app
ENTRYPOINT ["dotnet", "FileDisplay.dll"]
COPY input.txt /

COPY --from=builder /out/ .