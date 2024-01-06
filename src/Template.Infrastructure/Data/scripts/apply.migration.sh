#!/bin/bash

solutionName="Template"

dotnet ef \
    --startup-project ../../$solutionName.Api/ database update \
    -c ApplicationDbContext