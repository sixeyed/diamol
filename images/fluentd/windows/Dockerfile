# adapted from 
# https://github.com/fluent/fluentd-docker-image/blob/master/v1.8/windows/Dockerfile

FROM mcr.microsoft.com/windows/servercore:ltsc2019

RUN powershell -Command "Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))"

# Fluentd depends on cool.io whose fat gem is only available for Ruby < 2.5, so need to specify --platform ruby when install Ruby > 2.5 and install msys2 to get dev tools
RUN choco install -y ruby --version 2.6.5.1 --params "'/InstallDir:C:\ruby26'" \
&& choco install -y msys2 --params "'/NoPath /NoUpdate /InstallDir:C:\ruby26\msys64'"

RUN refreshenv \
&& ridk install 2 3 \
&& echo gem: --no-document >> C:\ProgramData\gemrc \
&& gem install cool.io -v 1.5.4 --platform ruby \
&& gem install oj -v 3.3.10 \
&& gem install json -v 2.2.0 \
&& gem install fluentd -v 1.8.0 \
&& gem install win32-service -v 1.0.1 \
&& gem install win32-ipc -v 0.7.0 \
&& gem install win32-event -v 0.6.3 \
&& gem install windows-pr -v 1.2.6 \
&& gem install fluent-plugin-elasticsearch -v 3.7.1 \
&& gem sources --clear-all

# Remove gem cache and chocolatey
RUN powershell -Command "Remove-Item -Force C:\ruby26\lib\ruby\gems\2.6.0\cache\*.gem; Remove-Item -Recurse -Force 'C:\ProgramData\chocolatey'"

COPY fluent.conf /fluent/conf/fluent.conf

ENV FLUENTD_CONF="fluent.conf"
EXPOSE 24224 5140
ENTRYPOINT ["cmd", "/k", "fluentd", "-c", "C:\\fluent\\conf\\fluent.conf"]