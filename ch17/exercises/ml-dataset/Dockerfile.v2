FROM diamol/base

ARG DATASET_URL=https://archive.ics.uci.edu/ml/machine-learning-databases/url/url_svmlight.tar.gz

WORKDIR /dataset

RUN wget -O dataset.tar.gz ${DATASET_URL} && \
    tar -xf dataset.tar.gz url_svmlight/Day1.svm && \
    rm -f dataset.tar.gz