using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ResetPassword;

public record ResetPasswordCommand(string Email, string Token, string Password)
    : IRequest<Result<object>>
{
    public ResetPasswordRequest ToDto() => new(Email, Token, Password);
};

public record ResetPasswordRequest(string Email, string Token, string Password);
