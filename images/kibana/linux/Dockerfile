FROM diamol/base:2e AS installer
ARG KIBANA_VERSION
ARG DOCKER_BUILD_CPU_ALT
WORKDIR /downloads

# find the downloads for previous versions here - https://www.elastic.co/downloads/past-releases#kibana-oss
# 7.10 is the latest version which is OSS, see - https://www.elastic.co/pricing/faq/licensing
# https://www.elastic.co/downloads/past-releases/kibana-oss-7-10-2
# https://artifacts.elastic.co/downloads/kibana/kibana-oss-7.10.2-linux-x86_64.tar.gz
# https://artifacts.elastic.co/downloads/kibana/kibana-oss-7.10.2-linux-aarch64.tar.gz

RUN echo "Downloading: https://artifacts.elastic.co/downloads/kibana/kibana-oss-${KIBANA_VERSION}-linux-${DOCKER_BUILD_CPU_ALT}.tar.gz" && \
    wget -O kibana.tar.gz https://artifacts.elastic.co/downloads/kibana/kibana-oss-${KIBANA_VERSION}-linux-${DOCKER_BUILD_CPU_ALT}.tar.gz

RUN mkdir /kibana && \
    tar -xzf kibana.tar.gz --strip-components=1 -C /kibana && \
    rm -rf /kibana/node

# Kibana requires Node.js - this image is the official Node distribution
FROM diamol/node:2e
RUN apk add --no-cache bash

EXPOSE 5601
ENV KIBANA_HOME="/usr/share/kibana" 

WORKDIR /usr/share/kibana
COPY --from=installer /kibana .
COPY kibana bin/
COPY kibana.yml config/

RUN chmod +x bin/kibana
CMD ["/usr/share/kibana/bin/kibana", "--allow-root"]