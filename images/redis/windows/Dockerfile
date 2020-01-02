FROM diamol/base as download
ARG REDIS_VERSION="3.0.504"

# https://github.com/microsoftarchive/redis/releases/download/win-3.0.504/Redis-x64-3.0.504.zip

RUN curl -L -o redis.zip https://github.com/microsoftarchive/redis/releases/download/win-%REDIS_VERSION%/Redis-x64-%REDIS_VERSION%.zip
RUN md redis && \
    tar -xzf redis.zip -C redis

# redis
FROM diamol/base

EXPOSE 6379
CMD ["redis-server"]

WORKDIR /redis
COPY --from=download /redis/ .