using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Extensions;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.IdentityServer.Constants.Errors;

namespace Template.Application.Identity.Commands.ExternalSignIn;

public class ExternalSignInCommandHandler(
    ILogger<ExternalSignInCommandHandler> logger,
    IIdentityService identityService
) : IRequestHandler<ExternalSignInCommand, Result<string>>
{
    private readonly ILogger<ExternalSignInCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<string>> Handle(
        ExternalSignInCommand request,
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
                return Result<string>.Failed(
                    ErrorCode.ERR_TOKEN,
                    authenticationResult.Failure.Message
                );

            var result = await _identityService.RegisterExternalAsync(authenticationResult);
            if (!result.Succeeded)
                return Result<string>.Failed(result.Errors.ToArray());

            var returnUrl = authenticationResult.FindReturnUrl();

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
