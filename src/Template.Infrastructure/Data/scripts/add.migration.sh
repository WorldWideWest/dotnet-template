#!/bin/bash

solutionName="Template"

dotnet ef \
    --project ../../../$solutionName.Infrastructure \
    --startup-project ../../../$solutionName.Api/ migrations add InitIdentityUser \
    --output-dir Data/Migrations/Identity \
    -c IdentityDbContext \
