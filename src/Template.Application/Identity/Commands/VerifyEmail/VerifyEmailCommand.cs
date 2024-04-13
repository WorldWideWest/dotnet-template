using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.VerifyEmail;

public record VerifyEmailCommand(string Email, string Token) : IRequest<Result<object>>
{
    public VerifyEmailRequest ToDto() => new(Email, Token);
};

public record VerifyEmailRequest(string Email, string Token);
