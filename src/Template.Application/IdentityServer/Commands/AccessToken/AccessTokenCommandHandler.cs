using MediatR;
using Template.Application.IdentityServer.Common;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.AccessToken;

public class AccessTokenCommandHandler : IRequestHandler<AccessTokenCommand, Result<TokenResultDto>>
{
    public Task<Result<TokenResultDto>> Handle(
        AccessTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
