FROM microsoft/mssql-server-linux:latest

MAINTAINER Jaskaranbir Dhillon

ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

COPY sql_db_script.sql run.sh /

CMD /bin/bash /run.sh & /opt/mssql/bin/sqlservr