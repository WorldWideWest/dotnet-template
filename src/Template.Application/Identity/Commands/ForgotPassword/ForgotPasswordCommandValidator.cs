using FluentValidation;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Identity.Constants.Regex;

namespace Template.Application.Identity.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(user => user.Email)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .Matches(Pattern.Email)
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyError("Email"));
    }
}
