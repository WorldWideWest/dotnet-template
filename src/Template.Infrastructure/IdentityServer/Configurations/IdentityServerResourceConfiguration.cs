using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using Template.Domain.Common.Models;
using DomainIdentityServerConstants = Template.Domain.IdentityServer.Constants;

namespace Template.Infrastructure.IdentityServer.Configurations;

public class IdentityServerResourceConfiguration
{
    /// <summary>
    /// Returns a collection of API scopes representing different permissions for accessing endpoints.
    /// </summary>
    /// <returns>An enumerable collection of API scopes.</returns>
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new()
            {
                Name = DomainIdentityServerConstants.ApiScope.Read,
                DisplayName = DomainIdentityServerConstants.ApiScope.Read,
                Description = "Authorized to use all GET endpoints",
                Required = true,
                UserClaims = new List<string> { JwtClaimTypes.Email, }
            },
            new()
            {
                Name = DomainIdentityServerConstants.ApiScope.Write,
                DisplayName = DomainIdentityServerConstants.ApiScope.Write,
                Description = "Authorized to use all POST endpoints",
                Required = true,
                UserClaims = new List<string> { JwtClaimTypes.Email, }
            },
            new()
            {
                Name = DomainIdentityServerConstants.ApiScope.Update,
                DisplayName = DomainIdentityServerConstants.ApiScope.Update,
                Description = "Authorized to use all PUT, PATCH endpoints",
                Required = true,
                UserClaims = new List<string> { JwtClaimTypes.Email, }
            },
            new()
            {
                Name = DomainIdentityServerConstants.ApiScope.Delete,
                DisplayName = DomainIdentityServerConstants.ApiScope.Delete,
                Description = "Authorized to use all DELETE endpoints",
                Required = true,
                UserClaims = new List<string> { JwtClaimTypes.Email, }
            },
            new()
            {
                Name = DomainIdentityServerConstants.ApiScope.Test,
                DisplayName = DomainIdentityServerConstants.ApiScope.Test,
                Description = "Testing Scope",
                Required = true,
                UserClaims = new List<string> { JwtClaimTypes.Email, }
            },
        };

    /// <summary>
    /// Returns a collection of API resources representing different resources and associated scopes.
    /// </summary>
    /// <returns>An enumerable collection of API resources.</returns>
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new()
            {
                Name = DomainIdentityServerConstants.ApiResource.Template,
                DisplayName = DomainIdentityServerConstants.ApiResource.Template,
                Scopes = new List<string>
                {
                    DomainIdentityServerConstants.ApiScope.Read,
                    DomainIdentityServerConstants.ApiScope.Write,
                    DomainIdentityServerConstants.ApiScope.Update,
                    DomainIdentityServerConstants.ApiScope.Delete,
                },
                UserClaims = new List<string> { JwtClaimTypes.Email }
            }
        };

    /// <summary>
    /// Returns a collection of client configurations based on the provided application configuration.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>An enumerable collection of client configurations.</returns>
    public static IEnumerable<Client> Clients(AppConfig configuration)
    {
        return new List<Client>
        {
            new()
            {
                ClientId = DomainIdentityServerConstants.ClientId.Web,
                ClientName = DomainIdentityServerConstants.ClientName.Web,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = true,
                AllowedScopes =
                {
                    DomainIdentityServerConstants.ApiScope.Read,
                    DomainIdentityServerConstants.ApiScope.Write,
                    DomainIdentityServerConstants.ApiScope.Update,
                    DomainIdentityServerConstants.ApiScope.Delete,
                },
                ClientSecrets = new List<Secret>
                {
                    new Secret(configuration.IdentityServerConfig.Clients.Web.Secret.Sha256()),
                },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                Enabled = true,
            },
            new()
            {
                ClientId = DomainIdentityServerConstants.ClientId.Mobile,
                ClientName = DomainIdentityServerConstants.ClientName.Mobile,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = true,
                AllowedScopes =
                {
                    DomainIdentityServerConstants.ApiScope.Read,
                    DomainIdentityServerConstants.ApiScope.Write,
                    DomainIdentityServerConstants.ApiScope.Update,
                    DomainIdentityServerConstants.ApiScope.Delete,
                },
                ClientSecrets = new List<Secret>
                {
                    new Secret(configuration.IdentityServerConfig.Clients.Mobile.Secret.Sha256()),
                },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                Enabled = true,
            }
        };
    }

    /// <summary>
    /// Returns a collection of identity resources representing standard OpenID Connect identity scopes.
    /// </summary>
    /// <returns>An enumerable collection of identity resources.</returns>
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };
}
