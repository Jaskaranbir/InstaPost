FROM node:alpine

MAINTAINER Jaskaranbir Dhillon

COPY run.sh /

RUN chmod +x /run.sh && \
    mkdir -p /usr/src/app
WORKDIR /usr/src/app

EXPOSE 8080

CMD ["/run.sh"]