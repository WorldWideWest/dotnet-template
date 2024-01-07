using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.CreateUser;

public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password)
    : IRequest<Result<object>>;

public record CreateUserDto(string FirstName, string LastName, string Email, string Password);
