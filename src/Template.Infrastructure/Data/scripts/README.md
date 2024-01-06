# Database Scripts

This folder contains the following scripts:
- `configure.sh` it is used to create the database, it is called by the `mssql-tools` service that creates the database on our sql server service.
- `add.migration.sh` use this scripts to create migrations files that will then be applied to the database
- `apply.migration.sh` apply your migrations to the database


## Running the scripts

Before running the scripts make sure you have `dotnet ef` installed:
```bash
dotnet tool install -g dotnet-ef
```

Running the scripts:
```bash
chmod +x ./add.migration.sh
./add.migration.sh

chmod +x ./apply.migration.sh
./apply.migration.sh
```

TBD:
    - Add powershell scripts to support this functionality on Windows