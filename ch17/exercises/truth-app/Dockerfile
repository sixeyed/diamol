FROM diamol/maven AS builder

WORKDIR /usr/src/truth
COPY pom.xml .
RUN mvn -B dependency:go-offline

COPY . .
RUN mvn package

# app
FROM diamol/openjdk:jdk

WORKDIR /app
COPY truth.txt .
COPY --from=builder /usr/src/truth/target/truth-app-0.1.0.jar .

# temp - for testing
COPY --from=builder /usr/src/truth/src/test/FileUpdateTest.java .

EXPOSE 80
ENTRYPOINT ["java", "-jar", "/app/truth-app-0.1.0.jar"]