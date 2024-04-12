using Duende.IdentityServer;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Authorization;
using Template.Domain.Identity.Entites;
using Template.Domain.IdentityServer.Constants.Authorization;
using Template.Infrastructure.Data;

namespace Template.Infrastructure.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentityExtension(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var migrationAssembly = typeof(IdentityExtension).Assembly.FullName;
        var settings = configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();

        services
            .AddAuthentication()
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = ApiResource.Template;
                    options.Authority = settings.IdentityServerConfig.Authority;

                    options.TokenValidationParameters.ValidateLifetime = true;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                }
            )
            .AddGoogle(
                GoogleDefaults.AuthenticationScheme,
                options =>
                {
                    var google = settings.IdentityServerConfig.Clients.GoogleWeb;

                    options.SignInScheme =
                        IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = google.ExternalClientId;
                    options.ClientSecret = google.ExternalClientSecret;
                }
            );

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                Policy.ReadAccess,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Read);
                }
            );

            options.AddPolicy(
                Policy.WriteAccess,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Write);
                }
            );

            options.AddPolicy(
                Policy.UpdateAccess,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Update);
                }
            );

            options.AddPolicy(
                Policy.DeleteAccess,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Delete);
                }
            );

            options.AddPolicy(
                Policy.UpdateProfilePasswordAccess,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Update);
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.UpdateProfilePassword);
                }
            );

            // MARK: Add default authentication handlers
            var defaultAuthorizationPolicy = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme,
                GoogleDefaults.AuthenticationScheme,
                IdentityServerConstants.DefaultCookieAuthenticationScheme
            );

            options.DefaultPolicy = defaultAuthorizationPolicy.RequireAuthenticatedUser().Build();
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
