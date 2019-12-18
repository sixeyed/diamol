FROM diamol/base as download
ARG KIBANA_VERSION="6.8.5"

# https://artifacts.elastic.co/downloads/kibana/kibana-oss-6.8.5-linux-x86_64.tar.gz

RUN wget "https://artifacts.elastic.co/downloads/kibana/kibana-oss-${KIBANA_VERSION}-linux-x86_64.tar.gz"
RUN tar -xzf "kibana-oss-${KIBANA_VERSION}-linux-x86_64.tar.gz"

WORKDIR /kibana-oss-${KIBANA_VERSION}-linux-x86_64
RUN rm -rf node

# kibana - we have to use 6.x because 7.x isn't compatible with Arm; 6.8.5 requires node@ 10.15.2
FROM diamol/node:10.15.2
ARG KIBANA_VERSION="6.8.5"

EXPOSE 5601
ENV KIBANA_HOME="/usr/share/kibana" 

WORKDIR /usr/share/kibana
COPY --from=download /kibana-${KIBANA_VERSION}-linux-x86_64/ .
COPY kibana bin/
COPY kibana.yml config/

RUN chmod +x bin/kibana
CMD ["bin/kibana"]