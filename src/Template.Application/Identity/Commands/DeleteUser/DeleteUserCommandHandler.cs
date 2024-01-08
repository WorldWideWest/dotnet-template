using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.DeleteUser;

public class DeleteUserCommandHandler(
    ILogger<DeleteUserCommandHandler> logger,
    IIdentityService identityService,
    IValidationFactory validationFactory
) : IRequestHandler<DeleteUserCommand, Result<object>>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;
    private readonly IValidationFactory _validationFactory = validationFactory;

    public async Task<Result<object>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return validationResult;

            var result = await _identityService.DeleteUserAsync(new(request.Email));
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
