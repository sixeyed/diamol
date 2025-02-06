FROM diamol/base:2e
COPY cmd.sh .
RUN chmod +x cmd.sh
CMD exec /bin/sh -c "trap : TERM INT; ./cmd.sh; (while true; do sleep 1000; done) & wait"