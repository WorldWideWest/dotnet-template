using MediatR;
using Template.Application.IdentityServer.Common;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.AccessToken;

public record AccessTokenCommand(
    string GrantType,
    string ClientId,
    string ClientSecret,
    string Email,
    string Password,
    string Scope
) : IRequest<Result<TokenResultDto>>
{
    public AccessTokenDto ToDto() => new(GrantType, ClientId, ClientSecret, Email, Password, Scope);
};

public record AccessTokenDto(
    string GrantType,
    string ClientId,
    string ClientSecret,
    string Email,
    string Password,
    string Scope
);
