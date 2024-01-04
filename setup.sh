#!/bin/bash

Solution='./Template.sln'
SolutionName='Template'

function initializeSolution()
{
    # More about creating a solution here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln?source=recommendations
    
    dotnet new sln --name $SolutionName
}

function createProjects()
{
    # Read more about the project types here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22#arguments
    
    echo -e "\e[36mCreating the project [$SolutionName.Api]\e[0m"
    dotnet new webapi -n $SolutionName.Api -o ./src/$SolutionName.Api

    echo -e "\e[36mCreating the project [$SolutionName.Application]\e[0m"
    dotnet new classlib -n $SolutionName.Application -o ./src/$SolutionName.Application

    echo -e "\e[36mCreating the project [$SolutionName.Domain]\e[0m"
    dotnet new classlib -n $SolutionName.Domain -o ./src/$SolutionName.Domain

    echo -e "\e[36mCreating the project [$SolutionName.Services]\e[0m"
    dotnet new classlib -n $SolutionName.Infrastructure -o ./src/$SolutionName.Infrastructure
}

function connectProjectsToSolution()
{
    # More info here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln?source=recommendations#add
    echo -e "\e[36mAdded [$SolutionName.Api] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Api/$SolutionName.Api.csproj

    echo -e "\e[36mAdded [$SolutionName.Application] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Application/$SolutionName.Application.csproj

    echo -e "\e[36mAdded [$SolutionName.Domain] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Domain/$SolutionName.Domain.csproj

    echo -e "\e[36mAdded [$SolutionName.Infrastructure] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Infrastructure/$SolutionName.Infrastructure.csproj

}

function addReferencesBetweenProjects()
{
    # Read more about it here https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-reference

    echo -e "\e[36mAdded [$SolutionName.Api] project references\e[0m"
    dotnet add ./src/$SolutionName.Api/$SolutionName.Api.csrpoj reference ./src/$SolutionName.Domain/$SolutionName.Domain.csrpoj
    dotnet add ./src/$SolutionName.Api/$SolutionName.Api.csrpoj reference ./src/$SolutionName.Application/$SolutionName.Application.csrpoj
    dotnet add ./src/$SolutionName.Api/$SolutionName.Api.csrpoj reference ./src/$SolutionName.Infrastructure/$SolutionName.Infrastructure.csrpoj

    echo -e "\e[36mAdded [$SolutionName.Infrastructure] project references\e[0m"
    dotnet add ./src/$SolutionName.Infrastructure/$SolutionName.Infrastructure.csrpoj reference ./src/$SolutionName.Application/$SolutionName.Application.csrpoj

    echo -e "\e[36mAdded [$SolutionName.Application] project references\e[0m"
    dotnet add ./src/$SolutionName.Application/$SolutionName.Application.csrpoj reference ./src/$SolutionName.Domain/$SolutionName.Domain.csrpoj
}

if [ ! -f "$Solution" ]; then
    initializeSolution
fi

if [ ! -d "src" ]; then
    createProjects
    connectProjectsToSolution
    addReferencesBetweenProjects
fi