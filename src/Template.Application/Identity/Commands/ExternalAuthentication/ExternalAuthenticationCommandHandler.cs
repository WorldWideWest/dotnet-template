using MediatR;
using Microsoft.AspNetCore.Authentication;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ExternalAuthentication;

public record ExternalAuthenticationCommand(AuthenticateResult Result) : IRequest<Result<string>>;
