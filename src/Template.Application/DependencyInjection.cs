using Microsoft.Extensions.DependencyInjection;
using Template.Application.Email.Interfaces;
using Template.Application.Email.Templates;

namespace Template.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<List<IEmailClassifier>>(_ =>
        {
            return new List<IEmailClassifier>()
            {
                new EmailVerificationTemplate(),
                new ResetPasswordTemplate(),
            };
        });

        return services;
    }
}
