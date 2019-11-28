@echo off

echo ----------------
echo Generating certs - hostname: %HOST_NAME%; IP: %HOST_IP%; expiry days: %EXPIRY_DAYS%
echo ----------------

openssl rand -base64 32 > ca.password

openssl genrsa -aes256 -passout file:ca.password -out ca-key.pem 4096
openssl req -subj "/C=UK/ST=LONDON/L=London/O=Diamol/OU=." -new -x509 -days %EXPIRY_DAYS% -passin file:ca.password -key ca-key.pem -sha256 -out ca.pem

openssl genrsa -out server-key.pem 4096
openssl req -subj "/CN=%HOST_NAME%" -sha256 -new -key server-key.pem -out server.csr

echo subjectAltName = DNS:%HOST_NAME%,IP:%HOST_IP%,IP:127.0.0.1 >> extfile.cnf
echo extendedKeyUsage = serverAuth >> extfile.cnf
openssl x509 -req -days %EXPIRY_DAYS% -sha256 -in server.csr -CA ca.pem -CAkey ca-key.pem -CAcreateserial -out server-cert.pem -extfile extfile.cnf -passin file:ca.password

openssl genrsa -out client-key.pem 4096
openssl req -subj "/CN=client" -new -key client-key.pem -out client.csr

echo extendedKeyUsage = clientAuth > extfile-client.cnf
openssl x509 -req -days %EXPIRY_DAYS% -sha256 -in client.csr -CA ca.pem -CAkey ca-key.pem -CAcreateserial -out client-cert.pem -extfile extfile-client.cnf -passin file:ca.password

del *.cnf
del *.csr
del *.srl

echo ----------------
echo Certs generated. 
echo ----------------

dir
exit /B 0