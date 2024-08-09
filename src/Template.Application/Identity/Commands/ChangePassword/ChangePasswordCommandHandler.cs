using MediatR;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<object>>
{
    private readonly IIdentityService _identityService;

    public ChangePasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<object>> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _identityService.ChangePasswordAsync(request.ToDto());
        if (!result.Succeeded)
            return result;

        return Result<object>.Success();
    }
}
