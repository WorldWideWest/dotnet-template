using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Email.Interfaces;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Email.Enums;

namespace Template.Application.Identity.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, Result<object, object>>
{
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(
        ILogger<ForgotPasswordCommandHandler> logger,
        IIdentityService identityService,
        IEmailService emailService
    )
    {
        _logger = logger;
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task<Result<object, object>> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var searchResult = await _identityService.FindUserAsync(new(request.Email));
        if (!searchResult.Succeeded)
            return Result<object, object>.Failed(searchResult.Errors.ToArray());

        var result = await _identityService.GenerateResetPasswordTokenAsync(new(request.Email));
        if (!result.Succeeded)
            return Result<object, object>.Failed(result.Errors.ToArray());

        var parameters = _emailService.GenerateResetPasswordParameters(
            searchResult.Data,
            result.Data
        );

        _emailService
            .SendAsync(EmailType.ResetPassword, searchResult.Data.Email, parameters)
            .ContinueWith(
                task => _logger.LogError(task.Exception, task.Exception.Message, nameof(Handle)),
                TaskContinuationOptions.OnlyOnFaulted
            );
        ;

        return Result<object, object>.Success();
    }
}
