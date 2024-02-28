using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Email.Interfaces;
using Template.Application.Email.Templates;
using Template.Application.Validation.Interfaces;
using Template.Application.Validation.Services;

namespace Template.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddSingleton<List<IEmailClassifier>>(_ =>
        {
            return new List<IEmailClassifier>()
            {
                new EmailVerificationTemplate(),
                new ResetPasswordTemplate(),
            };
        });

        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(assembly);
        });

        services.AddTransient<IValidationFactory, ValidationFactory>();

        return services;
    }
}
