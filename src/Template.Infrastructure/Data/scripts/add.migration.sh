#!/bin/bash

solutionName="Template"

dotnet ef \
    --project ../../../$solutionName.Infrastructure \
    --startup-project ../../../$solutionName.Api/ migrations add InitIdentityUser \
    --output-dir Data/Migrations/Identity \
    -c IdentityDbContext \

dotnet ef \
    --project ../../../$solutionName.Infrastructure \
    --startup-project ../../../$solutionName.Api/ migrations add InitialIdentityServerPersistedGrantDbMigration \
    --output-dir Data/Migrations/IdentityServer/PersistedGrantDb \
    -c PersistedGrantDbContext \

dotnet ef \
    --project ../../../$solutionName.Infrastructure \
    --startup-project ../../../$solutionName.Api/ migrations add InitialIdentityServerConfigurationDbMigration \
    --output-dir Data/Migrations/IdentityServer/ConfigurationDb \
    -c ConfigurationDbContext \