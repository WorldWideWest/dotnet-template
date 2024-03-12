using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ExternalSignIn;

public record ExternalSignInCommand(HttpContext HttpContext) : IRequest<Result<string>>;
