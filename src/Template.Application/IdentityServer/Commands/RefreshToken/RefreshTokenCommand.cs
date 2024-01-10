using MediatR;
using Template.Application.IdentityServer.Common;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.RefreshToken;

public record RefreshTokenCommand(
    string RefreshToken,
    string ClientId,
    string ClientSecret,
    string Scope
) : IRequest<Result<TokenResultDto>>
{
    public RefreshTokenDto ToDto() => new(RefreshToken, ClientId, ClientSecret, Scope);
};

public record RefreshTokenDto(
    string RefreshToken,
    string ClientId,
    string ClientSecret,
    string Scope
);
