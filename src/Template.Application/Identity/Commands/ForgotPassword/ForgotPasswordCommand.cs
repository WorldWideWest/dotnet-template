using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<Result<object>>;
