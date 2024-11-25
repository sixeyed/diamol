ARG PROMETHEUS_VERSION
FROM prom/prometheus:v${PROMETHEUS_VERSION}

USER root
ENTRYPOINT []

CMD echo "$DOCKER_HOST  DOCKER_HOST" >> /etc/hosts && \
    /bin/prometheus \
    --storage.tsdb.path=/prometheus \
    --web.console.libraries=/etc/prometheus/console_libraries \
    --web.console.templates=/etc/prometheus/consoles \
    --config.file=/etc/prometheus/prometheus.yml

COPY prometheus.yml /etc/prometheus/prometheus.yml