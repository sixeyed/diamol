FROM diamol/base:2e AS build-stage
RUN echo 'Building...' > /build.txt

FROM diamol/base:2e AS test-stage
COPY --from=build-stage /build.txt /build.txt
RUN echo 'Testing...' >> /build.txt

FROM diamol/base:2e
COPY --from=test-stage /build.txt /build.txt
CMD ["cat", "/build.txt"]