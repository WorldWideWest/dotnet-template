using MediatR;
using Microsoft.AspNetCore.Authentication;
using Template.Domain.Common.Models;

public record ExternalSignOutCommand(string LogoutId) : IRequest<Result<string, object>>;

public record ExternalSignOutResponse(AuthenticationProperties Properties, string IdentityProvider);
