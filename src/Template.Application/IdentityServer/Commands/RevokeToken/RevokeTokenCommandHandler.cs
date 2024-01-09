using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.IdentityServer.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.RevokeToken;

public class RevokeTokenCommandHandler(
    ILogger<RevokeTokenCommandHandler> logger,
    ITokenService tokenService,
    IValidationFactory validationFactory
) : IRequestHandler<RevokeTokenCommand, Result<object>>
{
    private readonly ILogger<RevokeTokenCommandHandler> _logger = logger;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IValidationFactory _validationFactory = validationFactory;

    public async Task<Result<object>> Handle(
        RevokeTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return Result<object>.Failed(validationResult.Errors.ToArray());

            return await _tokenService.RevokeTokenAsync(request.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
