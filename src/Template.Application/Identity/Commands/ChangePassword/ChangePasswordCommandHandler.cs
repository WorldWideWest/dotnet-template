using MediatR;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<object>>
{
    private readonly IIdentityService _identityService;
    private readonly IValidationFactory _validationFactory;

    public ChangePasswordCommandHandler(
        IIdentityService identityService,
        IValidationFactory validationFactory
    )
    {
        _identityService = identityService;
        _validationFactory = validationFactory;
    }

    public async Task<Result<object>> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validationFactory.ValidateAsync(request);
        if (!validationResult.Succeeded)
            return validationResult;

        var result = await _identityService.ChangePasswordAsync(request.ToDto());
        if (!result.Succeeded)
            return result;

        return Result<object>.Success();
    }
}
