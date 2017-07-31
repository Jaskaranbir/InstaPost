#!/bin/bash

if [ ! -f db_imported ]; then
  echo "Running Database setup script..."

  RET=1
  while [[ RET -ne 0 ]]; do
    echo "=> Waiting for confirmation of SQLServer service startup"
    sleep 5
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P ${SA_PASSWORD} >/dev/null 2>&1
    RET=$?
  done

  /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P ${SA_PASSWORD} -i '/sql_db_script.sql'
  touch db_imported
fi

echo "Database setup script complete."
echo
echo "This server is now ready to connect."
