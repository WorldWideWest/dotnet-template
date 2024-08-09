using MediatR;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result<object>>
{
    private readonly IIdentityService _identityService;

    public VerifyEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<object>> Handle(
        VerifyEmailCommand request,
        CancellationToken cancellationToken
    )
    {
        var searchResult = await _identityService.FindUserAsync(new(request.Email));
        if (!searchResult.Succeeded)
            return Result<object>.Failed(searchResult.Errors.ToArray());

        var result = await _identityService.VerifyEmailAsync(request.ToDto());
        if (!result.Succeeded)
            return result;

        return Result<object>.Success();
    }
}
