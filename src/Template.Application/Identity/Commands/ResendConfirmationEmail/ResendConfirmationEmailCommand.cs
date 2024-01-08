using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ResendConfirmationEmail;

public record ResendConfirmationEmailCommand(string Email) : IRequest<Result<object>>;
