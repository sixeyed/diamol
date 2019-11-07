FROM golang:1.13-alpine AS builder

ARG GOGS_VERSION="v0.11.91"

RUN apk --no-cache --no-progress add --virtual \
    build-deps \
    build-base \
    git \
    linux-pam-dev 

WORKDIR /go/src/github.com/gogs
RUN git clone https://github.com/gogs/gogs.git && \
    cd gogs && \
    git checkout $GOGS_VERSION

WORKDIR /go/src/github.com/gogs/gogs
RUN go build -tags "sqlite" -o /out/gogs 

# Gogs - adapted from project Dockerfile at github.com/gogs/gogs
FROM diamol/base

# Install system utils & Gogs runtime dependencies
ADD https://github.com/tianon/gosu/releases/download/1.10/gosu-amd64 /usr/sbin/gosu
RUN chmod +x /usr/sbin/gosu \
    && echo http://dl-2.alpinelinux.org/alpine/edge/community/ >> /etc/apk/repositories \
    && apk --no-cache --no-progress add \
    bash \
    ca-certificates \
    curl \
    git \
    linux-pam \
    openssh \
    s6 \
    shadow \
    socat \
    tzdata

COPY --from=builder /go/src/github.com/gogs/gogs/docker/nsswitch.conf /etc/nsswitch.conf

WORKDIR /app/gogs
COPY --from=builder /go/src/github.com/gogs/gogs/docker ./docker
COPY --from=builder /go/src/github.com/gogs/gogs/templates ./templates
COPY --from=builder /go/src/github.com/gogs/gogs/public ./public
COPY --from=builder /out/gogs .

RUN ./docker/finalize.sh

VOLUME ["/data"]
EXPOSE 3000
ENTRYPOINT ["/app/gogs/docker/start.sh"]
CMD ["/bin/s6-svscan", "/app/gogs/docker/s6/"]

COPY app.ini ./custom/conf/app.ini