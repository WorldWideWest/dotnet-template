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
            .WithErrorCode(Code.PropertyError("EMAIL"))
            .WithMessage(Message.PropertyEmpty("Email"))
            .NotNull()
            .WithErrorCode(Code.PropertyError("EMAIL"))
            .WithMessage(Message.PropertyEmpty("Email"))
            .Matches(Pattern.Email)
            .WithErrorCode(Code.PropertyError("EMAIL"))
            .WithMessage(Message.PropertyError("Email"));

        RuleFor(request => request.Token)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(Code.PropertyError("TOKEN"))
            .WithMessage(Message.PropertyEmpty("Token"))
            .NotNull()
            .WithErrorCode(Code.PropertyError("TOKEN"))
            .WithMessage(Message.PropertyEmpty("Token"));
    }
}
