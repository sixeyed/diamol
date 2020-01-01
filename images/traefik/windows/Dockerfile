FROM diamol/base

EXPOSE 80 443
ENTRYPOINT ["/traefik"]

COPY --from=traefik:v2.1.1-windowsservercore-1809 /windows/system32/netapi32.dll /windows/system32/netapi32.dll
COPY --from=traefik:v2.1.1-windowsservercore-1809 /traefik.exe /traefik.exe
