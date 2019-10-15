FROM diamol/node

ENV MAX_ALLOCATION_MB=4096 \
    LOOP_ALLOCATION_MB=512 \
    LOOP_INTERVAL_MS=2000

CMD node memory-check.js && \
    node memory-hog.js

HEALTHCHECK --interval=5s \
 CMD node memory-check.js

WORKDIR /app
COPY src/ .