using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Extensions;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ExternalAuthentication;

public class ExternalAuthenticationCommandHandler(
    ILogger<ExternalAuthenticationCommandHandler> logger,
    IIdentityService identityService
) : IRequestHandler<ExternalAuthenticationCommand, Result<string>>
{
    private readonly ILogger<ExternalAuthenticationCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<string>> Handle(
        ExternalAuthenticationCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            /*
                TODO: Investigate if this is the way to use the http context provided by the controller or find a workaround for using the http context accessor inside mediatr
            */
            var httpContext = request.HttpContext;

            var authenticationResult = await httpContext.AuthenticateWithExternalScheme();

            if (!authenticationResult.Succeeded)
                return Result<string>.Failed("", ""); // TODO: Adding real response status

            var result = await _identityService.RegisterExternalAsync(authenticationResult);
            if (!result.Succeeded)
                return Result<string>.Failed(result.Errors.ToArray());

            var returnUrl = authenticationResult.FindExternalUrl();

            await httpContext.DeleteCookieForExternalAuthentication();

            return Result<string>.Success(returnUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
