using Microsoft.AspNetCore.Http;
using Template.Application.IdentityServer.Common;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Interfaces;

public interface IExternalProvider
{
    bool Classify(string returnUrl);

    Result<AuthenticationPropertiesResponse> GetAuthenticationProperties(
        string returnUrl,
        HttpRequest request
    );
}
