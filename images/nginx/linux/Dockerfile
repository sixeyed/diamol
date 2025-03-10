ARG NGINX_VERSION
FROM nginx:${NGINX_VERSION}-alpine
RUN mkdir -p /data/nginx/cache/long /data/nginx/cache/short
ENTRYPOINT ["nginx", "-g", "daemon off;"]
COPY nginx.conf /etc/nginx/