using MediatR;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;

namespace Template.Application.Identity.Commands.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password)
    : IRequest<Result<object>>
{
    public CreateUserDto ToDto() => new(FirstName, LastName, Email, Password);
}

public record CreateUserDto(string FirstName, string LastName, string Email, string Password)
{
    public static User ToEntity(CreateUserDto dto) =>
        new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email
        };
}
