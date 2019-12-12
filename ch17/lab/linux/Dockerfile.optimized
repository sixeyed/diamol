FROM alpine:3.10 AS install
RUN apk add --no-cache docker-cli

FROM diamol/base
COPY --from=install /usr/bin/docker /usr/bin/docker