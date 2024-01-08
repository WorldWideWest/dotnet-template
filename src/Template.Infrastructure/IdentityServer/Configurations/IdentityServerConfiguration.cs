using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;

namespace Template.Infrastructure.IdentityServer.Configurations;

public static class IdentityServerConfiguration
{
    public static IServiceCollection ConfigureIdentityServer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var migrationAssembly = typeof(IdentityServerConfiguration).Assembly.FullName;
        var settings = configuration.GetSection("AppConfig").Get<AppConfig>();

        services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.UserInteraction.LoginUrl = "/api/Identity/external/login";
                options.UserInteraction.LogoutUrl = "/api/Identity/external/logout";

                options.Authentication.CookieLifetime = TimeSpan.FromDays(30);
                options.Authentication.CookieSlidingExpiration = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = context =>
                    context.UseSqlServer(
                        connectionString,
                        sql => sql.MigrationsAssembly(migrationAssembly)
                    );
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = context =>
                    context.UseSqlServer(
                        connectionString,
                        sql => sql.MigrationsAssembly(migrationAssembly)
                    );
                options.EnableTokenCleanup = true;
            })
            .AddAspNetIdentity<User>();

        services.AddSingleton<IDiscoveryCache>(cache =>
        {
            var factory = cache.GetRequiredService<IHttpClientFactory>();

            return new DiscoveryCache(
                settings.IdentityServerConfig.Authority,
                () => factory.CreateClient()
            );
        });

        return services;
    }
}
