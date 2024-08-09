using MediatR;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<object>>
{
    private readonly IIdentityService _identityService;
    private readonly IValidationFactory _validationFactory;

    public ResetPasswordCommandHandler(
        IIdentityService identityService,
        IValidationFactory validationFactory
    )
    {
        _identityService = identityService;
        _validationFactory = validationFactory;
    }

    public async Task<Result<object>> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await _validationFactory.ValidateAsync(request);
        if (!validationResult.Succeeded)
            return validationResult;

        var searchResult = await _identityService.FindUserAsync(new(request.Email));
        if (!searchResult.Succeeded)
            return Result<object>.Failed(searchResult.Errors.ToArray());

        var result = await _identityService.ResetPasswordAsync(request.ToDto());
        if (!result.Succeeded)
            return result;

        return Result<object>.Success();
    }
}
