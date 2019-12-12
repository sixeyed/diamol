FROM diamol/base

ENV JENKINS_HOME="/data" \
    JENKINS_VERSION="2.190.2"

COPY ./jenkins.install.UpgradeWizard.state ${JENKINS_HOME}/

WORKDIR /jenkins
RUN wget -O jenkins.war http://mirrors.jenkins.io/war-stable/${JENKINS_VERSION}/jenkins.war 

EXPOSE 8080
ENTRYPOINT java -Duser.home=${JENKINS_HOME} -Djenkins.install.runSetupWizard=false -jar /jenkins/jenkins.war
