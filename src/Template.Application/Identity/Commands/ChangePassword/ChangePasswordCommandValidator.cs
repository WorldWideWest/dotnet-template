using FluentValidation;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Identity.Constants.Regex;

namespace Template.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(user => user.OldPassword)
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

        RuleFor(user => user.NewPassword)
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
    }
}
