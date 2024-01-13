# Dotnet Template


## General info
This is a .NET Template that includes the implementation of [AspNetCore Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio) and [Duende IdentityServer](https://duendesoftware.com/products/identityserver) so that you can expand on it and build your own applications with it, or use it as an authorization server. 

The project contains integration test to check the functionality of the implementation and a local Compose environment for developing your app in a containerised environment.

The Solution is structured using the CLEAN Architecture Pattern.

As the time goes we will add support to other things, like debugging in a containerised environment for amd64 architecture, scripts for generating the projects, solutions and certificates on Windows.

### Initializing similar Solution structure without any implementation:

To initialize the same Solution structure like this one, go to the [scripts](./scripts/README.md) where you can find the configuration, implementation details and improvements.

## Setup
Initializing Blank Solution with all of it's projects
```bash
chmod +x ./setup.sh && ./setup.sh
```
TBD:
- Adding a Powershell script to support this on Windows

## Tooling
### [Local Development](./compose/README.md)

To use this functionality just run the following command:
```bash
docker-compose -f compose/docker-compose.debug.arm.yml up --build
```

To down the containers run the command below
```bash
docker-compose -f compose/docker-compose.debug.arm.yml down
```


### Configure code formatter for .net

Go into the console and install the `csharper` package globally:
```bash
dotnet tool install --global csharpier
```

Then install the following [extension](https://marketplace.visualstudio.com/items?itemName=emeraldwalk.RunOnSave) from VS Code store. 

This will enable you to format your code on save. Inside the `.vscode/settings.json` you have this configuration which will run the command on save which formatts all `.cs` files:
```json
{
  "emeraldwalk.runonsave": {
    "commands": [
      {
        "match": "\\.cs$",
        "isAsync": true,
        "cmd": "dotnet csharpier ."
      }
    ]
  }
}
```

