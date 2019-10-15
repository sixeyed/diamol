FROM diamol/node

ENV MAX_ALLOCATION_MB=4096 \
    LOOP_ALLOCATION_MB=512 \
    LOOP_INTERVAL_MS=2000

CMD ["node", "memory-hog.js"]

WORKDIR /app
COPY src/ .