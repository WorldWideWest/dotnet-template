using Duende.IdentityServer.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;

public class ExternalSignOutCommandHandler(
    ILogger<ExternalSignOutCommandHandler> logger,
    IIdentityServerInteractionService interaction,
    IOptions<AppConfig> options,
    SignInManager<User> signInManager
) : IRequestHandler<ExternalSignOutCommand, Result<string>>
{
    private readonly ILogger<ExternalSignOutCommandHandler> _logger = logger;
    private readonly IIdentityServerInteractionService _interaction = interaction;
    private readonly AppConfig _options = options.Value;
    private readonly SignInManager<User> _signInManager = signInManager;

    public async Task<Result<string>> Handle(
        ExternalSignOutCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var logoutId =
                request.LogoutId
                ?? await _interaction.CreateLogoutContextAsync().ConfigureAwait(false);

            var context = await _interaction.GetLogoutContextAsync(logoutId).ConfigureAwait(false);

            var postLogoutUri =
                context.PostLogoutRedirectUri
                ?? _options.IdentityServerConfig.Clients.GoogleWeb.PostLogoutRedirectUri;

            await _signInManager.SignOutAsync();

            return Result<string>.Success(postLogoutUri);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
