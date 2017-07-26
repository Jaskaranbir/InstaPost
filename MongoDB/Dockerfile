FROM mongo:latest

MAINTAINER Jaskaranbir Dhillon

ENV AUTH yes
ENV JOURNALING yes

RUN mkdir -p /data/db && \
    chown -R mongodb:mongodb /data && ls -l /

USER mongodb

ADD run.sh set_mongodb_password.sh mongo_db_script.sh /

EXPOSE 27017 28017

CMD ["/run.sh"]
