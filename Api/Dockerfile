FROM microsoft/dotnet:latest

MAINTAINER Jaskaranbir Dhillon

ENV ASPNETCORE_URLS http://*:5000

COPY run.sh /

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

EXPOSE 5000/tcp

CMD ["/run.sh"]