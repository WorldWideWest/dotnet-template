using FluentValidation;
using Template.Application.Validation.Extensions.IdentityServer;
using Template.Domain.Common.Constants.Errors;

namespace Template.Application.IdentityServer.Commands.AccessToken;

public class AccessTokenCommandValidator : AbstractValidator<AccessTokenCommand>
{
    public AccessTokenCommandValidator()
    {
        RuleFor(request => request.GrantType)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("GRANT_TYPE"))
            .WithMessage(ErrorMessage.PropertyEmpty("Grant Type"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("GRANT_TYPE"))
            .WithMessage(ErrorMessage.PropertyEmpty("Grant Type"))
            .IsGrantTypeSupported()
            .WithErrorCode(ErrorCode.PropertyError("GRANT_TYPE"))
            .WithMessage(ErrorMessage.PropertyError("Grant Type"));

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

        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("USERNAME"))
            .WithMessage(ErrorMessage.PropertyEmpty("Username"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("USERNAME"))
            .WithMessage(ErrorMessage.PropertyEmpty("Username"));

        RuleFor(request => request.Password)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("PASSWORD"))
            .WithMessage(ErrorMessage.PropertyEmpty("Password"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("PASSWORD"))
            .WithMessage(ErrorMessage.PropertyEmpty("Password"));

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
