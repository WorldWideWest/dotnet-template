using Template.Api.Configurations;
using Template.Api.Infrastructure;

namespace Template.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.ConfigureSwagger().ConfigureApiVersioning();

        return services;
    }
}
