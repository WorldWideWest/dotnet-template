using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Template.Application.Identity.Extensions;

public static class ExternalAuthenticationExstension
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

    public static string FindExternalUrl(this AuthenticateResult result) =>
        result.Properties.Items["returnUrl"] ?? "~/";
}
