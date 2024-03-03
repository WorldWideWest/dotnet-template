using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ExternalAuthentication;

public record ExternalAuthenticationCommand() : IRequest<Result<string>>;
