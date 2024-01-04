# Dotnet Template


## General info

## Setup
Initializing Blank Solution with all of it's projects
```bash
chmod +x ./setup.sh
./setup.sh
```
TBD:
- Adding project references between the projects (following the [CLEAN](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) architecture projec reference guide) 
- Adding a Powershell script to support this on Windows

## Tooling
### Running the app in Debug mode

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
docker-compose -f compose/docker-compose.debug.arm.yml up --build && docker-compose -f compose/docker-compose.debug.arm.yml down
```

TBD:
- Compose file for AMD64 architecture

### Configure code formatter for .net

Go into the console and install the `csharper` package globally:
```bash
dotnet tool install --global csharpier
```

Then install the following [extension](https://marketplace.visualstudio.com/items?itemName=emeraldwalk.RunOnSave) from VS Code store. 

This will enable you to format your code on save.

