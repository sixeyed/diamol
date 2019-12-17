# fluentd/Dockerfile
FROM fluent/fluentd:v1.8.0-debian-1.0
USER root
RUN ["gem", "install", "fluent-plugin-elasticsearch", "--no-document", "--version", "3.7.1"]
USER fluent