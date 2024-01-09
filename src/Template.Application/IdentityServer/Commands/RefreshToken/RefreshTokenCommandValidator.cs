using FluentValidation;
using Template.Application.Validation.Extensions.IdentityServer;
using Template.Domain.Common.Constants.Errors;

namespace Template.Application.IdentityServer.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(request => request.ClientId)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("CLIENT_ID"))
            .WithMessage(ErrorMessage.PropertyEmpty("Client Id"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("CLIENT_ID"))
            .WithMessage(ErrorMessage.PropertyEmpty("Client Id"));

        RuleFor(request => request.ClientSecret)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("CLIENT_SECRET"))
            .WithMessage(ErrorMessage.PropertyEmpty("Client Secret"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("CLIENT_SECRET"))
            .WithMessage(ErrorMessage.PropertyEmpty("Client Secret"));

        RuleFor(request => request.RefreshToken)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("REFRESH_TOKEN"))
            .WithMessage(ErrorMessage.PropertyEmpty("Refresh Token"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("REFRESH_TOKEN"))
            .WithMessage(ErrorMessage.PropertyEmpty("Refresh Token"));

        RuleFor(request => request.Scope)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("API_SCOPES"))
            .WithMessage(ErrorMessage.PropertyEmpty("Api Scopes"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("API_SCOPES"))
            .WithMessage(ErrorMessage.PropertyEmpty("Api Scopes"))
            .AreScopesSupported()
            .WithErrorCode(ErrorCode.PropertyError("API_SCOPES"))
            .WithMessage(ErrorMessage.PropertyEmpty("Api Scopes"));
    }
}
