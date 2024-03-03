using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ExternalAuthentication;

public record ExternalAuthenticationCommand(HttpContext HttpContext) : IRequest<Result<string>>;
