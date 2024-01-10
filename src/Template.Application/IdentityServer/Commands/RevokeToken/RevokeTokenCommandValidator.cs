using FluentValidation;
using Template.Domain.Common.Constants.Errors;

namespace Template.Application.IdentityServer.Commands.RevokeToken;

public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
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

        RuleFor(request => request.AccessToken)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("REFRESH_TOKEN"))
            .WithMessage(ErrorMessage.PropertyEmpty("Access Token"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("REFRESH_TOKEN"))
            .WithMessage(ErrorMessage.PropertyEmpty("Access Token"));
    }
}
