FROM diamol/base as download
ARG ELASTIC_VERSION="7.5.0"

# https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-7.5.0-no-jdk-linux-x86_64.tar.gz

RUN wget "https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-${ELASTIC_VERSION}-no-jdk-linux-x86_64.tar.gz"
RUN tar -xzf "elasticsearch-oss-${ELASTIC_VERSION}-no-jdk-linux-x86_64.tar.gz"

# elasticsearch
FROM diamol/openjdk
ARG ELASTIC_VERSION="7.5.0"

EXPOSE 9200 9300
ENV ES_HOME="/usr/share/elasticsearch" \
    ES_JAVA_OPTS="-Xms1024m -Xmx1024m"

RUN groupadd -g 1000 elasticsearch && \
    adduser --uid 1000 --gid 1000 --home /usr/share/elasticsearch elasticsearch

WORKDIR /usr/share/elasticsearch
COPY --from=download --chown=1000:0 /elasticsearch-${ELASTIC_VERSION}/ .
COPY elasticsearch.yml log4j2.properties config/

USER elasticsearch
CMD ["./bin/elasticsearch"]