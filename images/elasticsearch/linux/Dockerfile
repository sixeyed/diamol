FROM diamol/base:2e AS installer
ARG ELASTICSEARCH_VERSION
ARG DOCKER_BUILD_CPU_ALT
WORKDIR /downloads

# find the downloads for previous versions here - https://www.elastic.co/downloads/past-releases#elasticsearch-oss-no-jdk
# 7.10 is the latest version which is OSS, see - https://www.elastic.co/pricing/faq/licensing
# there's no no-jdk version for arm64, so we download the JDK and strip it out
# https://www.elastic.co/downloads/past-releases/elasticsearch-oss-7-10-2
# https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-7.10.2-linux-x86_64.tar.gz
# https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-7.10.2-linux-aarch64.tar.gz

RUN echo "Downloading: https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-${ELASTICSEARCH_VERSION}-linux-${DOCKER_BUILD_CPU_ALT}.tar.gz" && \
    wget -O elasticsearch.tar.gz "https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-oss-${ELASTICSEARCH_VERSION}-linux-${DOCKER_BUILD_CPU_ALT}.tar.gz" 

RUN mkdir /elasticsearch && \
    tar -xzf elasticsearch.tar.gz --strip-components=1 -C /elasticsearch && \
    rm -rf /elasticsearch/jdk

# Elasticsearch requires a JVM - this image provides a minimal JRE installation
# see the product-JVM version matrix https://www.elastic.co/support/matrix#matrix_jvm
FROM diamol/openjdk:2e-jre
RUN apk add --no-cache bash

WORKDIR /usr/share/elasticsearch
COPY --from=installer /elasticsearch .

EXPOSE 9200 9300
ENV ES_HOME="/usr/share/elasticsearch" \
    ES_JAVA_OPTS="-Xms1024m -Xmx1024m -Djava.security.manager=allow"

COPY elasticsearch.yml log4j2.properties ./config/

RUN addgroup -g 1000 elasticsearch && \
    adduser -D -u 1000 -G elasticsearch -h ${ES_HOME} elasticsearch && \
    chmod 0775 ${ES_HOME} && \
    chown -R 1000:0 ${ES_HOME}

USER elasticsearch:root
CMD ["/usr/share/elasticsearch/bin/elasticsearch"]