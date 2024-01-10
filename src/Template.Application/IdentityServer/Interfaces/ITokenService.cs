using Template.Application.IdentityServer.Commands.AccessToken;
using Template.Application.IdentityServer.Commands.RefreshToken;
using Template.Application.IdentityServer.Commands.RevokeToken;
using Template.Application.IdentityServer.Common;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Interfaces;

public interface ITokenService
{
    Task<Result<TokenResultDto>> RequestAccessTokenAsync(AccessTokenDto request);
    Task<Result<TokenResultDto>> RefreshTokenAsync(RefreshTokenDto request);
    Task<Result<object>> RevokeTokenAsync(RevokeTokenDto request);
}
