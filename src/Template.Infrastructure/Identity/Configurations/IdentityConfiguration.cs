using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Authorization;
using Template.Domain.Identity.Entites;
using Template.Domain.IdentityServer.Constants.Authorization;
using Template.Infrastructure.Data;

namespace Template.Infrastructure.Identity.Configurations;

public static class IdentityConfiguration
{
    public static IServiceCollection ConfigureIdentity(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var migrationAssembly = typeof(IdentityConfiguration).Assembly.FullName;
        var settings = configuration.GetSection("AppConfig").Get<AppConfig>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = "Template";
                    options.Authority = settings.IdentityServerConfig.Authority;
                }
            );

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                Policy.Read,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Read);
                }
            );

            options.AddPolicy(
                Policy.Write,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Write);
                }
            );

            options.AddPolicy(
                Policy.Update,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Update);
                }
            );

            options.AddPolicy(
                Policy.Delete,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Delete);
                }
            );
        });

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                sql => sql.MigrationsAssembly(migrationAssembly)
            );
        });

        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromDays(2);
        });

        return services;
    }
}
