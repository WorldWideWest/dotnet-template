using MediatR;
using Template.Application.Identity.Common;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Errors;

namespace Template.Application.Identity.Queries.GetProvider;

public class GetProviderQueryHandler
    : IRequestHandler<GetProviderQuery, Result<AuthenticationPropertiesResponse>>
{
    private readonly List<IExternalProvider> _providers;

    public GetProviderQueryHandler(List<IExternalProvider> providers)
    {
        _providers = providers;
    }

    public async Task<Result<AuthenticationPropertiesResponse>> Handle(
        GetProviderQuery request,
        CancellationToken cancellationToken
    )
    {
        var provider = _providers.Where(x => x.Classify(request.ReturnUrl)).FirstOrDefault();

        if (provider is null)
        {
            var error = new Error(ErrorCode.ProviderNotFound, ErrorMessage.ProviderNotFound);
            return Result<AuthenticationPropertiesResponse>.Failed(error);
        }

        await Task.FromResult(0);

        var properties = provider.GetAuthenticationProperties(request.ReturnUrl, request.Request);

        return Result<AuthenticationPropertiesResponse>.Success(properties.Body);
    }
}
