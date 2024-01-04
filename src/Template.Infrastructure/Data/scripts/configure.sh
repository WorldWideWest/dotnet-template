#!/bin/bash

# MSSQL_HOST & MSSQL_SA_PASSWORD = defined in compose env variables
/opt/mssql-tools/bin/sqlcmd -S $MSSQL_HOST -l 2000 -U sa -P $MSSQL_SA_PASSWORD -Q "CREATE DATABASE IdentityDb"