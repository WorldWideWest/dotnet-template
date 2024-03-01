using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Template.Application.IdentityServer.Common;
using Template.Application.IdentityServer.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.IdentityServer.Constants.Authorization;

namespace Template.Application.IdentityServer.Providers;

public class GoogleProvider : IExternalProvider
{
    public bool Classify(string returnUrl) =>
        returnUrl.Contains(IdentityProvider.Google, StringComparison.OrdinalIgnoreCase);

    public Result<AuthenticationPropertiesResponse> GetAuthenticationProperties(
        string returnUrl,
        HttpRequest request
    )
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = $"{request.Scheme}://{request.Host}/api/Identity/external/callback",
            Items = { { "returnUrl", returnUrl }, { "scheme", IdentityProvider.Google } }
        };

        var response = new AuthenticationPropertiesResponse(properties, IdentityProvider.Google);

        return Result<AuthenticationPropertiesResponse>.Success(response);
    }
}
