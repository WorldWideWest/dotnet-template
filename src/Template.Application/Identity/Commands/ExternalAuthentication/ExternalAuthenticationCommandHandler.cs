using Duende.IdentityServer;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ExternalAuthentication;

public class ExternalAuthenticationCommandHandler(
    ILogger<ExternalAuthenticationCommandHandler> logger,
    IIdentityService identityService,
    HttpContextAccessor httpContextAccessor
) : IRequestHandler<ExternalAuthenticationCommand, Result<string>>
{
    private readonly ILogger<ExternalAuthenticationCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;
    private readonly HttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<string>> Handle(
        ExternalAuthenticationCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var authenticationResult = await _httpContextAccessor
                .HttpContext.AuthenticateAsync(
                    IdentityServerConstants.ExternalCookieAuthenticationScheme
                )
                .ConfigureAwait(false);

            if (!authenticationResult.Succeeded)
                return Result<string>.Failed("", ""); // TODO: Adding real response status

            var result = await _identityService.RegisterExternalAsync(authenticationResult);
            if (!result.Succeeded)
                return Result<string>.Failed(result.Errors.ToArray());

            var returnUrl = authenticationResult.Properties.Items["returnUrl"] ?? "~/";

            await _httpContextAccessor
                .HttpContext.SignOutAsync(
                    IdentityServerConstants.ExternalCookieAuthenticationScheme
                )
                .ConfigureAwait(false);

            return Result<string>.Success(returnUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
