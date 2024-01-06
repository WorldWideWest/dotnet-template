using Template.Api.Configurations;

namespace Template.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.ConfigureSwagger();
        return services;
    }
}
