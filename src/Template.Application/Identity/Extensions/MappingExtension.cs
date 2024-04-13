using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Template.Application.Identity.Commands.CreateUser;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Authorization;
using Template.Domain.Identity.Entites;

namespace Template.Application.Identity.Extensions;

public static class MappingExtension
{
    /// <summary>
    /// Converts an <see cref="IdentityResult"/> to an array of <see cref="Error"/> objects.
    /// </summary>
    /// <param name="result">The <see cref="IdentityResult"/> to convert.</param>
    /// <returns>An array of <see cref="Error"/> objects.</returns>
    public static Error[] ToErrors(this IdentityResult result)
    {
        if (!result.Errors.Any())
            return [];

        return result.Errors.Select(x => new Error(x.Code, x.Description)).ToArray();
    }

    /// <summary>
    /// Converts a <see cref="ClaimsPrincipal"/> to a <see cref="User"/> entity.
    /// </summary>
    /// <param name="principal">The <see cref="ClaimsPrincipal"/> to convert.</param>
    /// <returns>A <see cref="User"/> entity.</returns>
    public static User ToEntity(this ClaimsPrincipal principal) =>
        new User
        {
            FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
            LastName = principal.FindFirstValue(ClaimTypes.Surname),
            UserName = principal.FindFirstValue(ClaimTypes.Email),
            Email = principal.FindFirstValue(ClaimTypes.Email),
            EmailConfirmed = true
        };

    /// <summary>
    /// Converts a <see cref="CreateUserRequest"/> object to a <see cref="User"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="CreateUserRequest"/> object to convert.</param>
    /// <returns>A <see cref="User"/> entity.</returns>
    public static User ToEntity(this CreateUserRequest dto) =>
        new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email
        };

    /// <summary>
    /// Selects the user ID from the authentication result.
    /// </summary>
    /// <param name="result">The <see cref="AuthenticateResult"/> to extract the user ID from.</param>
    /// <returns>The user ID as a string.</returns>
    public static string SelectUserId(this AuthenticateResult result) =>
        result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

    /// <summary>
    /// Selects the identity provider from the authentication result.
    /// </summary>
    /// <param name="result">The <see cref="AuthenticateResult"/> to extract the identity provider from.</param>
    /// <returns>The identity provider as a string.</returns>
    public static string SelectIdentityProvider(this AuthenticateResult result) =>
        result.Properties.Items[".AuthScheme"];

    /// <summary>
    /// Selects claims for the given <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The <see cref="User"/> whose claims are being selected.</param>
    /// <param name="provider">The identity provider for the claims (optional).</param>
    /// <returns>A list of <see cref="Claim"/> objects.</returns>
    public static List<Claim> SelectClaims(
        this User user,
        string provider = IdentityProvider.Local
    ) =>
        new List<Claim>
        {
            new Claim(JwtClaimTypes.Email, user.Email),
            new Claim(JwtClaimTypes.GivenName, user.FirstName),
            new Claim(JwtClaimTypes.FamilyName, user.LastName),
            new Claim(JwtClaimTypes.IdentityProvider, provider),
            new Claim(JwtClaimTypes.Name, user.Email),
        };
}
