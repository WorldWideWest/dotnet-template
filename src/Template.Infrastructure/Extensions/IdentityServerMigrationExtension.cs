using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Common.Models;
using Template.Infrastructure.Data;

namespace Template.Infrastructure.Extensions;

public class IdentityServerMigrationExtension
{
    public static async Task ApplyMigrationsAsync(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await scope.ServiceProvider.GetRequiredService<IdentityDbContext>().Database.MigrateAsync();

        await scope
            .ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
            .Database.MigrateAsync();

        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        await context.Database.MigrateAsync();

        await EnsureSeedConfiguration(context, app.Configuration);
    }

    private static async Task EnsureSeedConfiguration(
        ConfigurationDbContext context,
        IConfiguration configuration
    )
    {
        var settings = configuration.GetSection("AppConfig").Get<AppConfig>();

        foreach (var scope in IdentityServerResourceExtension.ApiScopes)
        {
            var exists = context.ApiScopes.Any(x => x.Name == scope.Name);
            if (!exists)
                await context.ApiScopes.AddAsync(scope.ToEntity());
        }

        foreach (var resource in IdentityServerResourceExtension.IdentityResources)
        {
            var exists = context.IdentityResources.Any(x => x.Name == resource.Name);
            if (!exists)
                await context.IdentityResources.AddAsync(resource.ToEntity());
        }

        foreach (var resource in IdentityServerResourceExtension.ApiResources)
        {
            var exists = context.ApiResources.Any(x => x.Name == resource.Name);
            if (!exists)
                await context.ApiResources.AddAsync(resource.ToEntity());
        }

        foreach (var client in IdentityServerResourceExtension.Clients(settings))
        {
            var exists = context.Clients.Any(x => x.ClientId == client.ClientId);
            if (!exists)
                await context.Clients.AddAsync(client.ToEntity());
        }

        await context.SaveChangesAsync();
    }
}
