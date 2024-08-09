using Duende.IdentityServer.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;

public class ExternalSignOutCommandHandler : IRequestHandler<ExternalSignOutCommand, Result<string>>
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly AppConfig _options;
    private readonly SignInManager<User> _signInManager;

    public ExternalSignOutCommandHandler(
        IIdentityServerInteractionService interaction,
        IOptions<AppConfig> options,
        SignInManager<User> signInManager
    )
    {
        _interaction = interaction;
        _options = options.Value;
        _signInManager = signInManager;
    }

    public async Task<Result<string>> Handle(
        ExternalSignOutCommand request,
        CancellationToken cancellationToken
    )
    {
        var logoutId =
            request.LogoutId ?? await _interaction.CreateLogoutContextAsync().ConfigureAwait(false);

        var context = await _interaction.GetLogoutContextAsync(logoutId).ConfigureAwait(false);

        var postLogoutUri =
            context.PostLogoutRedirectUri
            ?? _options.IdentityServerConfig.Clients.GoogleWeb.PostLogoutRedirectUri;

        await _signInManager.SignOutAsync();

        return Result<string>.Success(postLogoutUri);
    }
}
