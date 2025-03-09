ARG FLUENTD_VERSION
ARG FLUENTD_RELEASE
ARG DOCKER_BUILD_CPU
FROM fluent/fluentd:${FLUENTD_VERSION}-debian-${DOCKER_BUILD_CPU}-${FLUENTD_RELEASE}

COPY fluent.conf /fluent/conf/fluent.conf
ENV FLUENTD_CONF="fluent.conf"

USER root

RUN gem update --system && \
    gem install fluent-plugin-elasticsearch --no-document

USER fluent