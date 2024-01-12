#!/bin/bash

solution="./Template.sln"
solutionName="Template"

function initializesolution()
{
    # More about creating a solution here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln?source=recommendations
    dotnet new sln --name $solutionName
}

function createProjects()
{
    # Read more about the project types here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22#arguments
    echo -e "\e[36mCreating the project [$solutionName.Api]\e[0m"
    dotnet new webapi \
        -n $solutionName.Api \
        -o ./src/$solutionName.Api

    echo -e "\e[36mCreating the project [$solutionName.Application]\e[0m"
    dotnet new classlib \
        -n $solutionName.Application \
        -o ./src/$solutionName.Application

    echo -e "\e[36mCreating the project [$solutionName.Domain]\e[0m"
    dotnet new classlib \
        -n $solutionName.Domain \
        -o ./src/$solutionName.Domain

    echo -e "\e[36mCreating the project [$solutionName.Infrastructure]\e[0m"
    dotnet new classlib \
        -n $solutionName.Infrastructure \
        -o ./src/$solutionName.Infrastructure

    echo -e "\e[36mCreating the project [$solutionName.Infrastructure]\e[0m"
    dotnet new xunit \
        -n $solutionName.Application.UnitTests \
        -o ./tests/$solutionName.Application.UnitTests
}

function connectProjectsTosolution()
{
    # More info here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln?source=recommendations#add
    echo -e "\e[36mAdded [$solutionName.Api] to [$solutionName] solution\e[0m"
    dotnet sln $solutionName.sln add \
        ./src/$solutionName.Api/$solutionName.Api.csproj

    echo -e "\e[36mAdded [$solutionName.Application] to [$solutionName] solution\e[0m"
    dotnet sln $solutionName.sln add \
        ./src/$solutionName.Application/$solutionName.Application.csproj

    echo -e "\e[36mAdded [$solutionName.Domain] to [$solutionName] solution\e[0m"
    dotnet sln $solutionName.sln add \
        ./src/$solutionName.Domain/$solutionName.Domain.csproj

    echo -e "\e[36mAdded [$solutionName.Infrastructure] to [$solutionName] solution\e[0m"
    dotnet sln $solutionName.sln add \
        ./src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj

    echo -e "\e[36mAdded [$solutionName.Infrastructure] to [$solutionName] solution\e[0m"
    dotnet sln $solutionName.sln add \
        ./tests/$solutionName.Application.UnitTests/$solutionName.Application.UnitTests.csproj

}

function addReferencesBetweenProjects()
{
    # Read more about it here https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-reference
    echo -e "\e[36mAdded [$solutionName.Api] project references\e[0m"

    dotnet add ./src/$solutionName.Api/$solutionName.Api.csproj reference \
        ./src/$solutionName.Domain/$solutionName.Domain.csproj

    dotnet add ./src/$solutionName.Api/$solutionName.Api.csproj reference \
        ./src/$solutionName.Application/$solutionName.Application.csproj

    dotnet add ./src/$solutionName.Api/$solutionName.Api.csproj reference \
        ./src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj

    echo -e "\e[36mAdded [$solutionName.Infrastructure] project references\e[0m"

    dotnet add ./src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj reference \
        ./src/$solutionName.Application/$solutionName.Application.csproj

    echo -e "\e[36mAdded [$solutionName.Application] project references\e[0m"
    
    dotnet add ./src/$solutionName.Application/$solutionName.Application.csproj reference \
        ./src/$solutionName.Domain/$solutionName.Domain.csproj
}

if [ ! -f "$solution" ]; then
    initializesolution
fi

if [ ! -d "src" ]; then
    createProjects
    connectProjectsTosolution
    addReferencesBetweenProjects
fi