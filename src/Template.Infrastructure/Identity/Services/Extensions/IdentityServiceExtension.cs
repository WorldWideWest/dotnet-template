using System.Security.Claims;
using Microsoft.Identity.Client;
using Template.Domain.Identity.Entites;

namespace Template.Infrastructure.Identity.Services.Extensions;

public static class IdentityServiceExtension
{
    public static string FindUserId(this AuthenticationResult result) =>
        result.ClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

    public static User ToEntity(this ClaimsPrincipal principal) =>
        new User
        {
            FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
            LastName = principal.FindFirstValue(ClaimTypes.Surname),
            UserName = principal.FindFirstValue(ClaimTypes.Email),
            Email = principal.FindFirstValue(ClaimTypes.Email),
            EmailConfirmed = true
        };
}
