using MediatR;
using Template.Application.IdentityServer.Common;
using Template.Application.IdentityServer.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Queries.GetProvider;

public class GetProviderQueryHandler(List<IExternalProvider> providers)
    : IRequestHandler<GetProviderQuery, Result<AuthenticationPropertiesResponse>>
{
    private readonly List<IExternalProvider> _providers = providers;

    public async Task<Result<AuthenticationPropertiesResponse>> Handle(
        GetProviderQuery request,
        CancellationToken cancellationToken
    )
    {
        var provider = _providers.Where(x => x.Classify(request.ReturnUrl)).FirstOrDefault();

        if (provider is null)
        {
            // TODO: ADd constants for this types of errors
            var error = new Error { Code = "", Description = " " };
            return Result<AuthenticationPropertiesResponse>.Failed(error);
        }

        await Task.FromResult(0);

        var properties = provider.GetAuthenticationProperties(request.ReturnUrl, request.Request);

        return Result<AuthenticationPropertiesResponse>.Success(properties.Body);
    }
}
