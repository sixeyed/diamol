FROM openjdk:11-jre-slim

RUN apt-get update \
 && apt-get install -y --no-install-recommends \
    curl \
 && rm -rf /var/lib/apt/lists/*