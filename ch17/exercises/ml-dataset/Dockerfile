FROM diamol/base

ARG DATASET_URL=https://archive.ics.uci.edu/ml/machine-learning-databases/url/url_svmlight.tar.gz

WORKDIR /dataset

RUN wget -O dataset.tar.gz ${DATASET_URL} && \
    tar xvzf dataset.tar.gz

WORKDIR /dataset/url_svmlight
RUN cp Day1.svm Day1.bak && \
    rm -f *.svm && \
    mv Day1.bak Day1.svm