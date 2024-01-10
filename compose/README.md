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
chmod +x ./certificates.sh
./certificates.sh
```
TBD:
- PowerShell script for generating certificates on Windows

## Running the containers
To mimic a deployment environment we added support for docker compose and debugging on it, simply we are running `dotnet watch run` inside our container so that every change gets replicated in the container. 

To use this functionality just run the following command:
```bash
docker-compose -f compose/docker-compose.debug.arm.yml up --build
```

To down the containers run the command below
```bash
docker-compose -f compose/docker-compose.debug.arm.yml down
```

And to make it convinient run:
```bash
docker-compose -f compose/docker-compose.debug.arm.yml down && docker-compose -f compose/docker-compose.debug.arm.yml up --build
```

TBD:
- Compose file for AMD64 architecture