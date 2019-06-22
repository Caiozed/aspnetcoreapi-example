FROM mysql:5.7.22
EXPOSE 3306
COPY ./AspNetCoreApiExample/DB/migrations/ /home/database
COPY ./AspNetCoreApiExample/ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh

