using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Template.Api.Configurations;

public static class ApiVersioningConfiguration
{
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("version")
            );
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VW";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
