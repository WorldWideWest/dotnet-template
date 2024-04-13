using Template.Api.Infrastructure;

namespace Template.Api.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddSwaggerExtension().AddApiVersioningExtension();

        return services;
    }
}
