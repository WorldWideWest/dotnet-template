using Microsoft.AspNetCore.Authentication;

namespace Template.Application.IdentityServer.Common;

public record AuthenticationPropertiesResponse(
    AuthenticationProperties Properties,
    string Provider
);
