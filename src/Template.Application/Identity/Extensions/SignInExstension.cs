using System.Security.Claims;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Template.Application.Extensions;

public static class SignInExtension
{
    public static async Task<AuthenticateResult> AuthenticateWithExternalScheme(
        this HttpContext httpContext
    )
    {
        return await httpContext
            .AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme)
            .ConfigureAwait(false);
    }

    public static async Task DeleteCookieForExternalAuthentication(this HttpContext httpContext)
    {
        await httpContext
            .SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme)
            .ConfigureAwait(false);
    }

    public static string FindReturnUrl(this AuthenticateResult result) =>
        result.Properties.Items["returnUrl"] ?? "~/";

    public static string FindIdentityProvider(this ClaimsPrincipal principal) =>
        principal.FindFirstValue("idp");
}
