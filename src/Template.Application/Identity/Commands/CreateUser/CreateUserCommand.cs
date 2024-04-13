using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password)
    : IRequest<Result<object>>
{
    public CreateUserRequest ToDto() => new(FirstName, LastName, Email, Password);
}

public record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
