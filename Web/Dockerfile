FROM node:slim

MAINTAINER Jaskaranbir Dhillon

COPY run.sh /

RUN mkdir -p /usr/src/app

RUN apt-get update && \
		apt-get install -y build-essential libssl-dev libffi-dev python-dev

WORKDIR /usr/src/app

EXPOSE 8080

CMD ["/run.sh"]