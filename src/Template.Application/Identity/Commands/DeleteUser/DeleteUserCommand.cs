using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.DeleteUser;

public record DeleteUserCommand(string Email) : IRequest<Result<object>>;
