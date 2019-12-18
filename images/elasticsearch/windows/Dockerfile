FROM diamol/base as download
ARG ELASTIC_VERSION="7.5.0"

# https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-7.5.0-no-jdk-windows-x86_64.zip

RUN curl -o elasticsearch.zip https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-%ELASTIC_VERSION%-no-jdk-windows-x86_64.zip
RUN tar -xzf elasticsearch.zip

# elasticsearch
FROM diamol/openjdk
ARG ELASTIC_VERSION="7.5.0"

EXPOSE 9200 9300
ENV ES_HOME="/usr/share/elasticsearch" \
    ES_JAVA_OPTS="-Xms1024m -Xmx1024m"

WORKDIR /usr/share/elasticsearch
COPY --from=download /elasticsearch-${ELASTIC_VERSION}/ .
COPY elasticsearch.yml log4j2.properties config/

USER ContainerAdministrator
CMD ["bin\\elasticsearch.bat"]