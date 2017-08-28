FROM node:alpine

MAINTAINER Jaskaranbir Dhillon

COPY run.sh /

RUN mkdir -p /usr/src/app

WORKDIR /usr/src/app

EXPOSE 8080

CMD ["/run.sh"]