using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.DeleteUser;

public class DeleteUserCommandHandler(
    ILogger<DeleteUserCommandHandler> _logger,
    IIdentityService _identityService,
    IValidationFactory _validationFactory
) : IRequestHandler<DeleteUserCommand, Result<object>>
{
    public async Task<Result<object>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
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
