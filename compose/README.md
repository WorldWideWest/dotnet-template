# Running Compose for Local Development

## General Info

This document describes the use of the compose files and the dependencies that need to be configured so it can work properly.

Dependencies:
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [OpenSSL](https://www.openssl.org/source/)

## Setup
Generate SSL certificates so `nginx` can consume them:
```bash
# cwd nginx
chmod +x ./certificates.sh && ./certificates.sh
```

## Running the containers
To mimic a deployment environment we added support for docker compose and debugging on it, simply we are running `dotnet watch run` inside our container so that every change gets replicated in the container. 

To use this functionality just run the following command:
```bash
docker-compose -f ./docker-compose.debug.arm.yml up --build
```

To down the containers run the command below
```bash
docker-compose -f ./docker-compose.debug.arm.yml down
```

And to make it convinient run:
```bash
docker-compose -f ./docker-compose.debug.arm.yml down && docker-compose -f ./docker-compose.debug.arm.yml up --build
```

## Improvements

| Status of Implementation      | Description | Issue |
| ----------- | ----------- |----------
| ❌      | Compose file for AMD64 architecture |
| ❌      | PowerShell script for generating certificates on Windows |
| ❌      | Debugging using VS Code Debugger | [#1](https://github.com/WorldWideWest/dotnet-template/issues/1)
| ❌      | Startup Script that generates SSL certificates and runs the dotnet watch command |
