#!/bin/bash

Solution='./Template.sln'
SolutionName='Template'
if [ ! -d "$Solution" ]
then
    # More about creating a solution here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln?source=recommendations
    dotnet new sln --name $SolutionName # initializing the solution

    # Read more about the project types here https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore22#arguments
    echo -e "\e[36mCreating the project [$SolutionName.Api]\e[0m"
    dotnet new webapi -n $SolutionName.Api -o ./src/$SolutionName.Api

    echo -e "\e[36mCreating the project [$SolutionName.Application]\e[0m"
    dotnet new classlib -n $SolutionName.Application -o ./src/$SolutionName.Application

    echo -e "\e[36mCreating the project [$SolutionName.Domain]\e[0m"
    dotnet new classlib -n $SolutionName.Domain -o ./src/$SolutionName.Domain

    echo -e "\e[36mCreating the project [$SolutionName.Services]\e[0m"
    dotnet new classlib -n $SolutionName.Infrastructure -o ./src/$SolutionName.Infrastructure

    # echo -e "\e[36mCreating the project [$SolutionName.Application.UnitTests]\e[0m"
    # dotnet new xunit -n $SolutionName.Application.UnitTests -o ./tests/$SolutionName.Application.UnitTests

    # echo -e "\e[36mCreating the project [$SolutionName.Infrastructure.UnitTests]\e[0m"
    # dotnet new xunit -n $SolutionName.Infrastructure.UnitTests -o ./tests/$SolutionName.Infrastructure.UnitTests

    # Connecting the projects to the solution https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln?source=recommendations#add
    echo -e "\e[36mAdded [$SolutionName.Api] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Api/$SolutionName.Api.csproj

    echo -e "\e[36mAdded [$SolutionName.Application] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Application/$SolutionName.Application.csproj

    echo -e "\e[36mAdded [$SolutionName.Domain] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Domain/$SolutionName.Domain.csproj

    echo -e "\e[36mAdded [$SolutionName.Infrastructure] to [$SolutionName] solution\e[0m"
    dotnet sln $SolutionName.sln add ./src/$SolutionName.Infrastructure/$SolutionName.Infrastructure.csproj

    # echo -e "\e[36mAdded [$SolutionName.Application.UnitTests] to [$SolutionName] solution\e[0m"
    # dotnet sln $SolutionName.sln add ./tests/$SolutionName.Application.UnitTests

    # echo -e "\e[36mAdded [$SolutionName.Application.UnitTests] to [$SolutionName] solution\e[0m"
    # dotnet sln $SolutionName.sln add ./tests/$SolutionName.Infrastructure.UnitTests

fi
