FROM mcr.microsoft.com/mssql/server

WORKDIR /temp/scripts
COPY StudentDatabaseScript.sql .
# ADD StudentDatabaseScript.sql /docker-entrypoint-initdb.d
#  CMD /bin/bash
# ENTRYPOINT [ "executable" ]
    # CMD /bin/bash   -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=arm!nder1" ` -p "1435:1433 