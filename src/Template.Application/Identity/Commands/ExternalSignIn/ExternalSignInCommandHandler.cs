using MediatR;
using Template.Application.Extensions;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.IdentityServer.Constants.Errors;

namespace Template.Application.Identity.Commands.ExternalSignIn;

public class ExternalSignInCommandHandler
    : IRequestHandler<ExternalSignInCommand, Result<string, object>>
{
    private readonly IIdentityService _identityService;

    public ExternalSignInCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<string, object>> Handle(
        ExternalSignInCommand request,
        CancellationToken cancellationToken
    )
    {
        var httpContext = request.HttpContext;

        var authenticationResult = await httpContext.AuthenticateWithExternalScheme();

        if (!authenticationResult.Succeeded)
            return Result<string, object>.Failed(
                ErrorCode.TokenError,
                authenticationResult.Failure.Message
            );

        var result = await _identityService.RegisterExternalAsync(authenticationResult);
        if (!result.Succeeded)
            return Result<string, object>.Failed(result.Errors.ToArray());

        var returnUrl = authenticationResult.FindReturnUrl();

        await httpContext.DeleteCookieForExternalAuthentication();

        return Result<string, object>.Success(returnUrl);
    }
}
