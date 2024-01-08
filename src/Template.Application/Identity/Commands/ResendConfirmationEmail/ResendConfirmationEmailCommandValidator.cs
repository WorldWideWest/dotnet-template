using FluentValidation;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Identity.Constants.Regex;

namespace Template.Application.Identity.Commands.ResendConfirmationEmail;

public class ResendConfirmationEmailCommandValidator
    : AbstractValidator<ResendConfirmationEmailCommand>
{
    public ResendConfirmationEmailCommandValidator()
    {
        RuleFor(user => user.Email)
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
    }
}
