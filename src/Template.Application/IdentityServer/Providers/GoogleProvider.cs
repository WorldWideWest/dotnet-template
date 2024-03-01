using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Template.Application.IdentityServer.Common;
using Template.Application.IdentityServer.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.IdentityServer.Constants.Authorization;

namespace Template.Application.IdentityServer.Providers;

public class GoogleProvider : IExternalProvider
{
    public bool Classify(string returnUrl) => returnUrl.Contains(Provider.Google);

    public Result<AuthenticationPropertiesResponse> GetAuthenticationProperties(
        string returnUrl,
        HttpRequest request
    )
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = $"{request.Scheme}://{request.Host}/api/Identity/external/callback",
            Items = { { "returnUrl", returnUrl }, { "scheme", Provider.Google } }
        };

        var response = new AuthenticationPropertiesResponse(properties, Provider.Google);

        return Result<AuthenticationPropertiesResponse>.Success(response);
    }
}
