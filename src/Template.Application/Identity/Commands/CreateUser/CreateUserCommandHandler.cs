using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.CreateUser;

public class CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger,
    IIdentityService identityService,
    IValidationFactory validationFactory
) : IRequestHandler<RegisterUserCommand, Result<object>>
{
    private readonly ILogger<CreateUserCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;
    private readonly IValidationFactory _validationFactory = validationFactory;

    public async Task<Result<object>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return validationResult;

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
