using MediatR;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Identity.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<object>>
{
    private readonly IIdentityService _identityService;

    public DeleteUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<object>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _identityService.DeleteUserAsync(new(request.Email));
        if (!result.Succeeded)
            return result;

        return Result<object>.Success();
    }
}
