using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Common.Models;
using Template.Infrastructure.Identity.Configurations;
using Template.Infrastructure.IdentityServer.Configurations;

namespace Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<AppConfig>(configuration.GetSection("AppConfig"));

        services.ConfigureIdentity(configuration);
        services.ConfigureIdentityServer(configuration);

        return services;
    }
}
