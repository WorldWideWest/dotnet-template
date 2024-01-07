using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Email.Interfaces;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;
using Template.Infrastructure.Email.Services;
using Template.Infrastructure.Identity.Configurations;
using Template.Infrastructure.Identity.Services;
using Template.Infrastructure.IdentityServer.Configurations;
using Template.Infrastructure.Validation.Services;

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

        services.AddScoped<IIdentityService, Identityservice>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddTransient<IValidationFactory, ValidationFactory>();

        return services;
    }
}
