using FluentValidation;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Identity.Constants.Regex;

namespace Template.Application.Identity.Commands.VerifyEmail;

public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("EMAIL"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("EMAIL"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .Matches(Pattern.Email)
            .WithErrorCode(ErrorCode.PropertyError("EMAIL"))
            .WithMessage(ErrorMessage.PropertyError("Email"));

        RuleFor(request => request.Token)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("TOKEN"))
            .WithMessage(ErrorMessage.PropertyEmpty("Token"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("TOKEN"))
            .WithMessage(ErrorMessage.PropertyEmpty("Token"));
    }
}
