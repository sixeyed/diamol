# adapted from
# https://github.com/fluent/fluentd-docker-image/blob/master/v1.8/debian/Dockerfile

FROM ruby:2.6.5-slim-buster
ENV TINI_VERSION=0.18.0

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    ca-certificates \
    && buildDeps=" \
    make gcc g++ libc-dev \
    wget bzip2 gnupg dirmngr \
    " \
    && apt-get install -y --no-install-recommends $buildDeps \
    && echo 'gem: --no-document' >> /etc/gemrc \
    && gem install oj -v 3.8.1 \
    && gem install json -v 2.2.0 \
    && gem install async-http -v 0.49.1 \
    && gem install fluentd -v 1.8.0 \
    && gem install fluent-plugin-elasticsearch -v 3.7.1 \
    && dpkgArch="$(dpkg --print-architecture | awk -F- '{ print $NF }')" \
    && wget -O /usr/local/bin/tini "https://github.com/krallin/tini/releases/download/v$TINI_VERSION/tini-$dpkgArch" \
    && wget -O /usr/local/bin/tini.asc "https://github.com/krallin/tini/releases/download/v$TINI_VERSION/tini-$dpkgArch.asc" \
    && export GNUPGHOME="$(mktemp -d)" \
    && gpg --batch --keyserver hkp://p80.pool.sks-keyservers.net:80 --recv-keys 6380DC428747F6C393FEACA59A84159D7001A4E5 \
    && gpg --batch --verify /usr/local/bin/tini.asc /usr/local/bin/tini \
    && rm -r /usr/local/bin/tini.asc \
    && chmod +x /usr/local/bin/tini \
    && tini -h \
    && wget -O /tmp/jemalloc-4.5.0.tar.bz2 https://github.com/jemalloc/jemalloc/releases/download/4.5.0/jemalloc-4.5.0.tar.bz2 \
    && cd /tmp && tar -xjf jemalloc-4.5.0.tar.bz2 && cd jemalloc-4.5.0/ \
    && ./configure && make \
    && mv lib/libjemalloc.so.2 /usr/lib \
    && apt-get purge -y --auto-remove \
    -o APT::AutoRemove::RecommendsImportant=false \
    $buildDeps \
    && rm -rf /var/lib/apt/lists/* \
    && rm -rf /tmp/* /var/tmp/* /usr/lib/ruby/gems/*/cache/*.gem

RUN groupadd -r fluent && useradd -r -g fluent fluent \
    # for log storage (maybe shared with host)
    && mkdir -p /fluentd/log \
    # configuration/plugins path (default: copied from .)
    && mkdir -p /fluentd/etc /fluentd/plugins \
    && chown -R fluent /fluentd && chgrp -R fluent /fluentd

COPY fluent.conf /fluentd/etc/
COPY entrypoint.sh /bin/
RUN chmod +x /bin/entrypoint.sh

ENV FLUENTD_CONF="fluent.conf"
ENV LD_PRELOAD="/usr/lib/libjemalloc.so.2"
EXPOSE 24224 5140

USER fluent
ENTRYPOINT ["tini",  "--", "/bin/entrypoint.sh"]
CMD ["fluentd"]
