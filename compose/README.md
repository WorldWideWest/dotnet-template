# Running Compose for Local Development

## General Info

This document describes the use of the compose files and the dependencies that need to be configured so it can work properly.

Dependencies:
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Setup

### Nginx
The Nginx Dockerfile is located in `./nginx/Dockerfile.debug` it is now a standalone Dockerfile which is modified to generate SSL certificates on the startup removing the need for the developer to do it manualy.

It runs the `./nginx/startup.sh` script which generates the certificates for localhost and after that it runs nginx.

The certificates are mapped to the volume and are located inside `./nginx/certs` && `./nginx/keys` so you can inspect them, they are also ignored from tracking.

### Running the containers
To mimic a deployment environment we added support for docker compose and debugging on it, simply we are running `dotnet watch run` inside our container so that every change gets replicated in the container. 

To use this functionality just run the following command:
```bash
# Arm based CPU's
docker-compose -f ./docker-compose.debug.arm.yml up --build

# x86 based CPU's (intel, amd)
docker-compose -f ./docker-compose.debug.amd.yml up --build
```

To down the containers run the command below
```bash
# Arm based CPU's
docker-compose -f ./docker-compose.debug.arm.yml down

# x86 based CPU's (intel, amd)
docker-compose -f ./docker-compose.debug.amd.yml down
```

And to make it convinient run:
```bash
# Arm based CPU's
docker-compose -f ./docker-compose.debug.arm.yml down && docker-compose -f ./docker-compose.debug.arm.yml up --build

# x86 based CPU's (intel, amd)
docker-compose -f ./docker-compose.debug.amd.yml down && docker-compose -f ./docker-compose.debug.amd.yml up --build
```

## Improvements

| Status of Implementation      | Description | Issue |
| ----------- | ----------- |----------
| ✅      | Compose file for AMD64 architecture | [#12](https://github.com/WorldWideWest/dotnet-template/issues/12)
| ❌      | Debugging using VS Code Debugger | [#1](https://github.com/WorldWideWest/dotnet-template/issues/1)
| ✅      | Startup Script that generates SSL certificates and runs the dotnet watch command | [#14](https://github.com/WorldWideWest/dotnet-template/issues/14)
