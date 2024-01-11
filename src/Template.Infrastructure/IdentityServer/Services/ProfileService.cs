using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Template.Domain.Identity.Entites;

namespace Template.Infrastructure.IdentityServer.Services;

public class ProfileService(ILogger<ProfileService> logger, UserManager<User> userManager)
    : IProfileService
{
    private readonly ILogger<ProfileService> _logger = logger;
    private readonly UserManager<User> _userManager = userManager;

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        try
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub).ConfigureAwait(false);

            var claims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);

            context.AddRequestedClaims(claims);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(GetProfileDataAsync));
            throw;
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        try
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);

            context.IsActive = user != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(IsActiveAsync));
            throw;
        }
    }
}
