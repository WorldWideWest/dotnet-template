using MediatR;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.RevokeToken;

public record RevokeTokenCommand(string AccessToken, string ClientId, string ClientSecret)
    : IRequest<Result<object>>
{
    public RevokeTokenDto ToDto() => new(AccessToken, ClientId, ClientSecret);
};

public record RevokeTokenDto(string AccessToken, string ClientId, string ClientSecret);
