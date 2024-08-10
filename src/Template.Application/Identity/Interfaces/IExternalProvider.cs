using Microsoft.AspNetCore.Http;
using Template.Application.Identity.Common;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Interfaces;

public interface IExternalProvider
{
    bool Classify(string returnUrl);

    Result<AuthenticationPropertiesResponse, object> GetAuthenticationProperties(
        string returnUrl,
        HttpRequest request
    );
}
