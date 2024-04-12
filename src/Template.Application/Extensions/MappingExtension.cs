using System.Security.Claims;
using FluentValidation.Results;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;
using Template.Domain.IdentityServer.Constants.Authorization;

namespace Template.Application.Extensions;

public static class MappingExtension
{
    /// <summary>
    /// Converts a <see cref="ValidationResult"/> to an array of <see cref="Error"/> objects.
    /// </summary>
    /// <param name="result">The <see cref="ValidationResult"/> to convert.</param>
    /// <returns>An array of <see cref="Error"/> objects.</returns>
    public static Error[] ToError(this ValidationResult result)
    {
        if (!result.Errors.Any())
            return [];

        return result.Errors.Select(x => new Error(x.ErrorCode, x.ErrorMessage)).ToArray();
    }

    /// <summary>
    /// Converts an <see cref="IdentityResult"/> to an array of <see cref="Error"/> objects.
    /// </summary>
    /// <param name="result">The <see cref="IdentityResult"/> to convert.</param>
    /// <returns>An array of <see cref="Error"/> objects.</returns>
    public static Error[] ToError(this IdentityResult result)
    {
        if (!result.Errors.Any())
            return [];

        return result.Errors.Select(x => new Error(x.Code, x.Description)).ToArray();
    }

    public static User ToEntity(this ClaimsPrincipal principal) =>
        new User
        {
            FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
            LastName = principal.FindFirstValue(ClaimTypes.Surname),
            UserName = principal.FindFirstValue(ClaimTypes.Email),
            Email = principal.FindFirstValue(ClaimTypes.Email),
            EmailConfirmed = true
        };

    public static string SelectUserId(this AuthenticateResult result) =>
        result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

    public static string SelectIdentityProvider(this AuthenticateResult result) =>
        result.Properties.Items[".AuthScheme"];

    public static List<Claim> SelectClaims(this User user, string provider = null) =>
        new List<Claim>
        {
            new Claim(JwtClaimTypes.Email, user.Email),
            new Claim(JwtClaimTypes.GivenName, user.FirstName),
            new Claim(JwtClaimTypes.FamilyName, user.LastName),
            new Claim(JwtClaimTypes.IdentityProvider, provider ?? IdentityProvider.Local),
            new Claim(JwtClaimTypes.Name, user.Email),
        };

    public static List<Claim> SelectClaims(
        this ClaimsPrincipal principal,
        string provider = null
    ) =>
        new List<Claim>
        {
            new Claim(JwtClaimTypes.Email, principal.FindFirstValue(ClaimTypes.Email)),
            new Claim(JwtClaimTypes.GivenName, principal.FindFirstValue(ClaimTypes.GivenName)),
            new Claim(JwtClaimTypes.FamilyName, principal.FindFirstValue(ClaimTypes.Surname)),
            new Claim(JwtClaimTypes.IdentityProvider, provider ?? IdentityProvider.Local),
            new Claim(JwtClaimTypes.Subject, principal.FindFirstValue(ClaimTypes.NameIdentifier)),
        };
}
