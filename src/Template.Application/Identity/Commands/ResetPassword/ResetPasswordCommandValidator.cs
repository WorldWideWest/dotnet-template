using FluentValidation;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Identity.Constants.Regex;

namespace Template.Application.Identity.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
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

        RuleFor(request => request.Password)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("PASSWORD"))
            .WithMessage(ErrorMessage.PropertyEmpty("Password"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("PASSWORD"))
            .WithMessage(ErrorMessage.PropertyEmpty("Password"))
            .Length(5, 15)
            .WithErrorCode(ErrorCode.PropertyError("PASSWORD"))
            .WithMessage(ErrorMessage.PropertyLength("Password", 5, 15))
            .Matches(Pattern.Password)
            .WithErrorCode(ErrorCode.PropertyError("PASSWORD"))
            .WithMessage(ErrorMessage.PropertyError("Password"));

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
