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
    private readonly ILogger<ExternalAuthenticationCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<string>> Handle(
        ExternalAuthenticationCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            // var result = await HttpContext
            return Result<string>.Success("");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
