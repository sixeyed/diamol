FROM nginx:1.17.6
RUN mkdir -p /data/nginx/cache/long /data/nginx/cache/short
ENTRYPOINT ["nginx", "-g", "daemon off;"]
COPY nginx.conf /etc/nginx/