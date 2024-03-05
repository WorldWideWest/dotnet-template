using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Template.Domain.Common.Models;

public record ExternalSignOutCommand(HttpContext HttpContext, ClaimsPrincipal User, string LogoutId)
    : IRequest<Result<string>>;

public record ExternalSignOutResponse(AuthenticationProperties Properties, string IdentityProvider);
