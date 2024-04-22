FROM debian:latest
WORKDIR /usr/share/nginx/html

RUN rm -rf ./*
RUN apt-get update
RUN apt-get upgrade
RUN apt-get -y install bash
RUN apt-get -y install nginx
RUN apt-get -y install nginx-full
RUN apt-get -y install nginx-extras
RUN adduser --system --no-create-home --shell /bin/false --group --disabled-login nginx
RUN chown -R nginx:nginx /var/www/html

CMD /bin/bash -c "nginx -t"
CMD /bin/bash -c "nginx -g 'daemon off;'"
