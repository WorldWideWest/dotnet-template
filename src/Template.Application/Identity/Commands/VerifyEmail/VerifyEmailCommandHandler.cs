using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.VerifyEmail;

public class VerifyEmailCommandHandler(
    ILogger<VerifyEmailCommandHandler> logger,
    IIdentityService identityService,
    IValidationFactory validationFactory
) : IRequestHandler<VerifyEmailCommand, Result<object>>
{
    private readonly ILogger<VerifyEmailCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;
    private readonly IValidationFactory _validationFactory = validationFactory;

    public async Task<Result<object>> Handle(
        VerifyEmailCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return validationResult;

            var searchResult = await _identityService.FindUserAsync(new(request.Email));
            if (!searchResult.Succeeded)
                return Result<object>.Failed(searchResult.Errors.ToArray());

            var result = await _identityService.VerifyEmailAsync(request.ToDto());
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
