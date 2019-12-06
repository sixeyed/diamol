FROM diamol/base:linux-amd64

RUN mkdir -p /logs && \
    cd /logs && \
    echo 1 > init.txt

RUN echo 2 >> /logs/init.txt

CMD head /logs/init.txt