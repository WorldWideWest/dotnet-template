using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Template.Application.IdentityServer.Commands.AccessToken;
using Template.Application.IdentityServer.Commands.RefreshToken;
using Template.Application.IdentityServer.Common;
using Template.Application.IdentityServer.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.IdentityServer.Constants.Errors;

namespace Template.Infrastructure.IdentityServer.Services;

public sealed class TokenService(
    ILogger<TokenService> logger,
    IHttpClientFactory httpClientFactory,
    IOptions<AppConfig> options
) : ITokenService
{
    private readonly ILogger<TokenService> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly AppConfig _options = options.Value;

    public async Task<Result<TokenResultDto>> RequestAccessTokenAsync(AccessTokenDto request)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var disco = await GetDiscoveryCacheAsync();
            if (!disco.IsError)
                return Result<TokenResultDto>.Failed(ErrorCode.ERR_TOKEN, disco.Error);

            var result = await client.RequestPasswordTokenAsync(
                new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = request.ClientId,
                    ClientSecret = request.ClientSecret,
                    Scope = request.Scope,
                    UserName = request.Email,
                    Password = request.Password
                }
            );

            if (result.IsError)
                return Result<TokenResultDto>.Failed(ErrorCode.ERR_TOKEN, disco.Error);

            return Result<TokenResultDto>.Success(
                new(result.AccessToken, result.ExpiresIn, result.RefreshToken)
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(RequestAccessTokenAsync));
            throw;
        }
    }

    public async Task<Result<TokenResultDto>> RequestAccessTokenFromRefreshTokenAsync(
        RefreshTokenDto request
    )
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var disco = await GetDiscoveryCacheAsync();
            if (!disco.IsError)
                return Result<TokenResultDto>.Failed(ErrorCode.ERR_TOKEN, disco.Error);

            var result = await client.RequestRefreshTokenAsync(
                new RefreshTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    RefreshToken = request.RefreshToken,
                    ClientId = request.ClientId,
                    ClientSecret = request.ClientSecret,
                    Scope = request.Scope
                }
            );

            if (result.IsError)
                return Result<TokenResultDto>.Failed(ErrorCode.ERR_TOKEN, disco.Error);

            return Result<TokenResultDto>.Success(
                new(result.AccessToken, result.ExpiresIn, result.RefreshToken)
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(RequestAccessTokenFromRefreshTokenAsync));
            throw;
        }
    }

    private async Task<DiscoveryDocumentResponse> GetDiscoveryCacheAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var disco = await client.GetDiscoveryDocumentAsync(
                new DiscoveryDocumentRequest()
                {
                    Address = _options.IdentityServerConfig.Authority,
                    Policy = { RequireHttps = false }
                }
            );

            return disco;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(GetDiscoveryCacheAsync));
            throw;
        }
    }
}
