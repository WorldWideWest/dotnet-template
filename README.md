# Dotnet Template


## General info
This is a .NET Template that includes the implementation of [AspNetCore Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio) and [Duende IdentityServer](https://duendesoftware.com/products/identityserver) so that you can expand on it and build your own applications with it, or use it as an authorization server. 

The project contains integration test to check the functionality of the implementation and a local Compose environment for developing your app in a containerised environment.

The Solution is structured using the CLEAN Architecture Pattern.

As the time goes we will add support to other things, like debugging in a containerised environment for amd64 architecture, scripts for generating the projects, solutions and certificates on Windows.

### Initializing similar Solution structure without any implementation:

To initialize the same Solution structure like this one, go to the [scripts](./scripts/README.md) where you can find the configuration, implementation details and improvements.

## Tooling
The tooling is mostly focuesed on VS Code and it's integration with thees tools, because I develeoped everything with it.

### Debugging
Follow the guide [here](./compose/README.md) to setup your environment for local development using Nginx, Docker, Compose and .NET 8

### Code Formatting

Go into the console and install the [csharper](https://csharpier.com/) package globally:
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

## CI/CD
This is implemented using GitHub Actions and the script can be found at `.github/workflows/main.yml`, currently it only runs the tests.

### Improvements
| Status of Implementation      | Description | Issue |
| ----------- | ----------- |----------
| ✅      | Creating Production Ready Dockerfile and implementation of uploading the image to Docker Hub | [#10](https://github.com/WorldWideWest/dotnet-template/issues/10)
| ✅ | Safely manage not awaited calls | [#22](https://github.com/WorldWideWest/dotnet-template/issues/22)
| ✅      | Global Error Handling | [#15](https://github.com/WorldWideWest/dotnet-template/issues/15)
|  ❌      | Create an Actual .NET Project Template | [#16](https://github.com/WorldWideWest/dotnet-template/issues/16)

