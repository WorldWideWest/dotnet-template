using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Email.Interfaces;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Email.Enums;

namespace Template.Application.Identity.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<object, object>>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public CreateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
        IIdentityService identityService,
        IEmailService emailService
    )
    {
        _logger = logger;
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task<Result<object, object>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _identityService.CreateUserAsync(request.ToDto());
        if (!result.Succeeded)
            return result;

        var searchResult = await _identityService.FindUserAsync(new(request.Email));
        if (!searchResult.Succeeded)
            return Result<object, object>.Failed(searchResult.Errors.ToArray());

        var verificationTokenResult = await _identityService.GenerateEmailVerificationTokenAsync(
            request.Email
        );
        if (!verificationTokenResult.Succeeded)
            return Result<object, object>.Failed(verificationTokenResult.Errors.ToArray());

        var parameters = _emailService.GenerateEmailConfirmationParameters(
            searchResult.Data,
            verificationTokenResult.Data
        );

        _emailService
            .SendAsync(EmailType.Verification, request.Email, parameters)
            .ContinueWith(
                task => _logger.LogError(task.Exception, task.Exception.Message, nameof(Handle)),
                TaskContinuationOptions.OnlyOnFaulted
            );

        return Result<object, object>.Success();
    }
}
