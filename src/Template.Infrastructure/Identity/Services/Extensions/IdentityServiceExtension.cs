using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Template.Domain.Identity.Entites;

namespace Template.Infrastructure.Identity.Services.Extensions;

public static class IdentityServiceExtension
{
    public static string FindUserId(this AuthenticateResult result) =>
        result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

    public static string FindIdentityProvider(this AuthenticateResult result) =>
        result.Properties.Items[".AuthScheme"];

    public static User ToEntity(this ClaimsPrincipal principal) =>
        new User
        {
            FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
            LastName = principal.FindFirstValue(ClaimTypes.Surname),
            UserName = principal.FindFirstValue(ClaimTypes.Email),
            Email = principal.FindFirstValue(ClaimTypes.Email),
            EmailConfirmed = true
        };

    public static List<Claim> SelectClaims(this User user) =>
        new List<Claim>
        {
            new Claim(JwtClaimTypes.Email, user.Email),
            new Claim(JwtClaimTypes.GivenName, user.FirstName),
            new Claim(JwtClaimTypes.FamilyName, user.LastName)
        };
}
