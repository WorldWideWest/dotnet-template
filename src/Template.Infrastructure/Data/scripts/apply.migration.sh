#!/bin/bash

solutionName="Template"

dotnet ef \
    --startup-project ../../../$solutionName.Api/ database update \
    -c ApplicationDbContext

dotnet ef --startup-project ../../../$solutionName.Api/ database update \
    -c PersistedGrantDbContext

dotnet ef --startup-project ../../../$solutionName.Api/ database update \
    -c ConfigurationDbContext