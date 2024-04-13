using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Template.Application.Identity.Common;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Constants;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Authorization;

namespace Template.Application.Identity.Providers;

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
            RedirectUri = $"{request.Scheme}://{request.Host}{IdentityDefaults.CallbackUrl}",
            Items = { { "returnUrl", returnUrl }, { "scheme", IdentityProvider.Google } }
        };

        var response = new AuthenticationPropertiesResponse(properties, IdentityProvider.Google);

        return Result<AuthenticationPropertiesResponse>.Success(response);
    }
}
