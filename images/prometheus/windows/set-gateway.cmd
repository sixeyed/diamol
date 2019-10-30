@echo off 

FOR /F "tokens=2 delims=:" %%A in ('ipconfig') do SET gateway=%%A
echo Setting gateway in resolv.conf: %gateway%
echo nameserver %gateway% > C:\etc\resolv.conf 