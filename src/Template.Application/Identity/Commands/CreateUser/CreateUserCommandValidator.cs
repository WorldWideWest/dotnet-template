using FluentValidation;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Identity.Constants.Regex;

namespace Template.Application.Identity.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<RegisterUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.FirstName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(Code.PropertyError("FIRST_NAME"))
            .WithMessage(Message.PropertyEmpty("First Name"))
            .NotNull()
            .WithErrorCode(Code.PropertyError("FIRST_NAME"))
            .WithMessage(Message.PropertyEmpty("First Name"))
            .Length(2, 50)
            .WithErrorCode(Code.PropertyError("FIRST_NAME"))
            .WithMessage(Message.PropertyLength("First Name", 2, 50))
            .Matches(Pattern.OnlyAlphabeticCharactersWithApostrophie)
            .WithErrorCode(Code.PropertyError("FIRST_NAME"))
            .WithMessage(Message.InvalidCharacters("First Name"));

        RuleFor(user => user.LastName)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(Code.PropertyError("LAST_NAME"))
            .WithMessage(Message.PropertyEmpty("Last Name"))
            .NotNull()
            .WithErrorCode(Code.PropertyError("LAST_NAME"))
            .WithMessage(Message.PropertyEmpty("Last Name"))
            .Length(2, 50)
            .WithErrorCode(Code.PropertyError("LAST_NAME"))
            .WithMessage(Message.PropertyLength("Last Name", 2, 50))
            .Matches(Pattern.OnlyAlphabeticCharactersWithApostrophie)
            .WithErrorCode(Code.PropertyError("LAST_NAME"))
            .WithMessage(Message.InvalidCharacters("Last Name"));

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

        RuleFor(user => user.Password)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
            .WithErrorCode(Code.PropertyError("PASSWORD"))
            .WithMessage(Message.PropertyEmpty("Password"))
            .NotNull()
            .WithErrorCode(Code.PropertyError("PASSWORD"))
            .WithMessage(Message.PropertyEmpty("Password"))
            .Length(5, 15)
            .WithErrorCode(Code.PropertyError("PASSWORD"))
            .WithMessage(Message.PropertyLength("Password", 5, 15))
            .Matches(Pattern.Password)
            .WithErrorCode(Code.PropertyError("PASSWORD"))
            .WithMessage(Message.PropertyError("Password"));
    }
}
