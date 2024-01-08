using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Email.Interfaces;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Email.Enums;

namespace Template.Application.Identity.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(
    ILogger<ForgotPasswordCommandHandler> logger,
    IIdentityService identityService,
    IValidationFactory validationFactory,
    IEmailService emailService
) : IRequestHandler<ForgotPasswordCommand, Result<object>>
{
    private readonly ILogger<ForgotPasswordCommandHandler> _logger = logger;
    private readonly IIdentityService _identityService = identityService;
    private readonly IValidationFactory _validationFactory = validationFactory;
    private readonly IEmailService _emailService = emailService;

    public async Task<Result<object>> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return validationResult;

            var searchResult = await _identityService.FindUserAsync(new(request.Email));
            if (!searchResult.Succeeded)
                return Result<object>.Failed(searchResult.Errors.ToArray());

            var result = await _identityService.GenerateResetPasswordTokenAsync(new(request.Email));
            if (!result.Succeeded)
                return Result<object>.Failed(result.Errors.ToArray());

            var parameters = _emailService.GenerateResetPasswordParameters(
                searchResult.Body,
                result.Body
            );

            _emailService.SendAsync(EmailType.ResetPassword, searchResult.Body.Email, parameters);

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
