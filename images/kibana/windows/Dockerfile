FROM diamol/base as download
ARG KIBANA_VERSION="6.8.5"

# https://artifacts.elastic.co/downloads/kibana/kibana-oss-6.8.5-windows-x86_64.zip

RUN curl -o kibana.zip https://artifacts.elastic.co/downloads/kibana/kibana-oss-%KIBANA_VERSION%-windows-x86_64.zip
RUN md C:\\kibana-%KIBANA_VERSION%-windows-x86_64 && \
    tar -xzf kibana.zip

WORKDIR /kibana-${KIBANA_VERSION}-windows-x86_64
RUN rm -f node\\node.exe

# kibana - we have to use 6.x because 7.x isn't compatible with Arm; 6.8.5 requires node@ 10.15.2
FROM diamol/node:10.15.2
ARG KIBANA_VERSION="6.8.5"

EXPOSE 5601
ENV KIBANA_HOME="/usr/share/kibana" 

WORKDIR /usr/share/kibana
COPY --from=download /kibana-${KIBANA_VERSION}-windows-x86_64/ .
COPY kibana.bat bin/
COPY kibana.yml config/

USER ContainerAdministrator
CMD ["bin\\kibana.bat"]