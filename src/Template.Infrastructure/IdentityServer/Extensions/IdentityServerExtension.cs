using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;
using Template.Infrastructure.IdentityServer.Services;

namespace Template.Infrastructure.IdentityServer.Extensions;

public static class IdentityServerExtension
{
    public static IServiceCollection AddIdentityServerConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var migrationAssembly = typeof(IdentityServerExtension).Assembly.FullName;
        var settings = configuration.GetSection("AppConfig").Get<AppConfig>();

        services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.Authentication.CookieLifetime = TimeSpan.FromDays(30);
                options.Authentication.CookieSlidingExpiration = true;
                options.IssuerUri = settings.IdentityServerConfig.IssuerUri;

                options.UserInteraction.LoginUrl = "/api/Identity/external/login";
                options.UserInteraction.LogoutUrl = "/api/Identity/external/logout";
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
            .AddProfileService<ProfileService>()
            .AddAspNetIdentity<User>();

        return services;
    }
}
