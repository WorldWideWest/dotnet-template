using FluentValidation;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Identity.Constants.Regex;

namespace Template.Application.Identity.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(user => user.FirstName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("FIRST_NAME"))
            .WithMessage(ErrorMessage.PropertyEmpty("First Name"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("FIRST_NAME"))
            .WithMessage(ErrorMessage.PropertyEmpty("First Name"))
            .Length(2, 50)
            .WithErrorCode(ErrorCode.PropertyError("FIRST_NAME"))
            .WithMessage(ErrorMessage.PropertyLength("First Name", 2, 50))
            .Matches(Pattern.OnlyAlphabeticCharactersWithApostrophie)
            .WithErrorCode(ErrorCode.PropertyError("FIRST_NAME"))
            .WithMessage(ErrorMessage.InvalidCharacters("First Name"));

        RuleFor(user => user.LastName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(ErrorCode.PropertyError("LAST_NAME"))
            .WithMessage(ErrorMessage.PropertyEmpty("Last Name"))
            .NotNull()
            .WithErrorCode(ErrorCode.PropertyError("LAST_NAME"))
            .WithMessage(ErrorMessage.PropertyEmpty("Last Name"))
            .Length(2, 50)
            .WithErrorCode(ErrorCode.PropertyError("LAST_NAME"))
            .WithMessage(ErrorMessage.PropertyLength("Last Name", 2, 50))
            .Matches(Pattern.OnlyAlphabeticCharactersWithApostrophie)
            .WithErrorCode(ErrorCode.PropertyError("LAST_NAME"))
            .WithMessage(ErrorMessage.InvalidCharacters("Last Name"));

        RuleFor(user => user.Email)
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

        RuleFor(user => user.Password)
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
