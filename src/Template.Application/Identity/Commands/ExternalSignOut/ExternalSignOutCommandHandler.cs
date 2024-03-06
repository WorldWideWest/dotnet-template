using Duende.IdentityServer.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Template.Domain.Common.Models;

public class ExternalSignOutCommandHandler(
    ILogger<ExternalSignOutCommandHandler> logger,
    IIdentityServerInteractionService interaction,
    IOptions<AppConfig> options
) : IRequestHandler<ExternalSignOutCommand, Result<string>>
{
    private readonly ILogger<ExternalSignOutCommandHandler> _logger = logger;
    private readonly IIdentityServerInteractionService _interaction = interaction;
    private readonly AppConfig _options = options.Value;

    public async Task<Result<string>> Handle(
        ExternalSignOutCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var httpContext = request.HttpContext;

            var logoutId =
                request.LogoutId
                ?? await _interaction.CreateLogoutContextAsync().ConfigureAwait(false);

            var context = await _interaction.GetLogoutContextAsync(logoutId).ConfigureAwait(false);

            var postLogoutUri =
                context.PostLogoutRedirectUri
                ?? _options.IdentityServerConfig.Clients.GoogleWeb.PostLogoutRedirectUri;

            await httpContext.SignOutAsync();

            return Result<string>.Success(postLogoutUri);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
