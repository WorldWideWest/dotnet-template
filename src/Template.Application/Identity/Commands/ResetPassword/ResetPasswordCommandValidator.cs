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
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyEmpty("Email"))
            .Matches(Pattern.Email)
            .WithErrorCode(ErrorCode.PropertyError("Email"))
            .WithMessage(ErrorMessage.PropertyError("Email"));

        RuleFor(request => request.Password)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("Password"))
            .WithMessage(ErrorMessage.PropertyEmpty("Password"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("Password"))
            .WithMessage(ErrorMessage.PropertyEmpty("Password"))
            .Length(5, 15)
            .WithErrorCode(ErrorCode.PropertyError("Password"))
            .WithMessage(ErrorMessage.PropertyLength("Password", 5, 15))
            .Matches(Pattern.Password)
            .WithErrorCode(ErrorCode.PropertyError("Password"))
            .WithMessage(ErrorMessage.PropertyError("Password"));

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
