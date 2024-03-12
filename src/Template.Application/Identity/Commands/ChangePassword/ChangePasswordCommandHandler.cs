using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    ILogger<ChangePasswordCommandHandler> logger,
    IIdentityService identityService,
    IValidationFactory validationFactory
) : IRequestHandler<ChangePasswordCommand, Result<object>>
{
    private readonly ILogger<ChangePasswordCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;
    private readonly IValidationFactory _validationFactory = validationFactory;

    public async Task<Result<object>> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return validationResult;

            var result = await _identityService.ChangePasswordAsync(request.ToDto());
            if (!result.Succeeded)
                return result;

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
