ARG ALPINE_VERSION
ARG MSSQL_VERSION
FROM alpine:${ALPINE_VERSION} AS installer

ARG SQLPACKAGE_DOWNLOAD_URL
RUN apk add --no-cache curl unzip
RUN curl -sSL -o sqlpackage.zip $SQLPACKAGE_DOWNLOAD_URL \
    && unzip -qq sqlpackage.zip -d /sqlpackage \
    && chmod +x /sqlpackage/sqlpackage


FROM mcr.microsoft.com/mssql/server:${MSSQL_VERSION}-latest

ARG MSSQL_SA_PASSWORD=sql!!s3rv3r
ENV ACCEPT_EULA=Y \
    MSSQL_SA_PASSWORD=${MSSQL_SA_PASSWORD} \
    SQLPACKAGE_HOME=/opt/sqlpackage

COPY --from=installer /sqlpackage/ /opt/sqlpackage/