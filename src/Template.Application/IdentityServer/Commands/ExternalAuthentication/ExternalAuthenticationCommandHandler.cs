using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.ExternalAuthentication;

public record ExternalAuthenticationCommand() : IRequest<Result<string>>;
