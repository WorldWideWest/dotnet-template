using Microsoft.AspNetCore.Authentication;

namespace Template.Application.Identity.Common;

public record AuthenticationPropertiesResponse(
    AuthenticationProperties Properties,
    string Provider
);
