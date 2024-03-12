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
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .Matches(Pattern.Email)
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyError("Email"));

        RuleFor(request => request.Token)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("Token"))
            .WithMessage(ErrorMessage.PropertyEmpty("Token"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("Token"))
            .WithMessage(ErrorMessage.PropertyEmpty("Token"));
    }
}
