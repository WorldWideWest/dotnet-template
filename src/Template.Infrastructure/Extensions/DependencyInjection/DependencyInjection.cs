using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Email.Interfaces;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Infrastructure.Email.Services;
using Template.Infrastructure.Identity.Services;

namespace Template.Infrastructure.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<AppConfig>(configuration.GetSection(nameof(AppConfig)));

        services.AddIdentityExtension(configuration).AddIdentityServerExtension(configuration);

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}
