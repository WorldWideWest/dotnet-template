using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.IdentityServer.Common;
using Template.Application.IdentityServer.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    ILogger<RefreshTokenCommandHandler> logger,
    ITokenService tokenService,
    IValidationFactory validationFactory
) : IRequestHandler<RefreshTokenCommand, Result<TokenResultDto>>
{
    private readonly ILogger<RefreshTokenCommandHandler> _logger = logger;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IValidationFactory _validationFactory = validationFactory;

    public async Task<Result<TokenResultDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return Result<TokenResultDto>.Failed(validationResult.Errors.ToArray());

            return await _tokenService.RefreshTokenAsync(request.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
