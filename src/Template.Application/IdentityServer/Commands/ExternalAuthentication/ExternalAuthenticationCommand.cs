using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.IdentityServer.Commands.ExternalAuthentication;

public class ExternalAuthenticationCommandHandler(
    ILogger<ExternalAuthenticationCommandHandler> logger,
    IIdentityService identityService
) : IRequestHandler<ExternalAuthenticationCommand, Result<string>>
{
    // TODO: Move to Identity
    private readonly ILogger<ExternalAuthenticationCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<string>> Handle(
        ExternalAuthenticationCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var result = await _identityService.RegisterExternalAsync(request.Result);
            if (!result.Succeeded)
                return Result<string>.Failed(result.Errors.ToArray());

            var returnUrl = request.Result.Properties.Items["returnUrl"] ?? "~/";

            return Result<string>.Success(returnUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
