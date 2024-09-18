ARG JENKINS_VERSION
ARG JENKINS_JDK_VERSION
FROM jenkins/jenkins:${JENKINS_VERSION}-alpine-${JENKINS_JDK_VERSION}

ENV LANG C.UTF-8

# jenkins deps
USER root
RUN apk add --no-cache \
    bash \
    coreutils \
    git \
    openssh-client \
    ttf-dejavu \
    unzip \
    docker-cli

ARG JENKINS_VERSION="2.263.4"
ENV JENKINS_VERSION=${JENKINS_VERSION} \
    JENKINS_HOME="/var/jenkins_home" \
    JENKINS_OPTS='-Djenkins.install.runSetupWizard=false'

VOLUME ${JENKINS_HOME}

EXPOSE 8080

COPY ./jenkins.install.UpgradeWizard.state ${JENKINS_HOME}/
COPY ./scripts/ ${JENKINS_HOME}/init.groovy.d/