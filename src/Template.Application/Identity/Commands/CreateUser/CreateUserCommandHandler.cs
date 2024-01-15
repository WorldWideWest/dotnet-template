using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Email.Interfaces;
using Template.Application.Identity.Interfaces;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Email.Enums;

namespace Template.Application.Identity.Commands.CreateUser;

public class CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> _logger,
    IIdentityService _identityService,
    IValidationFactory _validationFactory,
    IEmailService _emailService
) : IRequestHandler<CreateUserCommand, Result<object>>
{
    public async Task<Result<object>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var validationResult = await _validationFactory.ValidateAsync(request);
            if (!validationResult.Succeeded)
                return validationResult;

            var result = await _identityService.CreateUserAsync(request.ToDto());
            if (!result.Succeeded)
                return result;

            var searchResult = await _identityService.FindUserAsync(new(request.Email));
            if (!searchResult.Succeeded)
                return Result<object>.Failed(searchResult.Errors.ToArray());

            var verificationTokenResult =
                await _identityService.GenerateEmailVerificationTokenAsync(request.Email);
            if (!verificationTokenResult.Succeeded)
                return Result<object>.Failed(verificationTokenResult.Errors.ToArray());

            var parameters = _emailService.GenerateEmailConfirmationParameters(
                searchResult.Body,
                verificationTokenResult.Body
            );

            _emailService.SendAsync(EmailType.Verification, request.Email, parameters);

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Handle));
            throw;
        }
    }
}
