using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Template.Application.Email.Interfaces;
using Template.Application.Extensions;
using Template.Domain.Common.Models;
using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;
using Template.Domain.Identity.Entites;

namespace Template.Infrastructure.Email.Services;

public sealed class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly AppConfig _options;
    private readonly List<IEmailClassifier> _classifier;

    public EmailService(
        ILogger<EmailService> logger,
        IOptions<AppConfig> options,
        List<IEmailClassifier> classifier
    )
    {
        _logger = logger;
        _options = options.Value;
        _classifier = classifier;
    }

    public ClassifiedEmail Classify(EmailType type, Dictionary<string, string> paramters)
    {
        try
        {
            foreach (var classifier in _classifier)
            {
                bool classified = classifier.Classified(type);
                if (classified)
                    return classifier.GetEmail(paramters);
            }

            throw new Exception("Email can not be classified");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Classify));
            throw;
        }
    }

    public async Task SendAsync(EmailType type, string email, Dictionary<string, string> paramters)
    {
        try
        {
            ClassifiedEmail classifiedEmail = Classify(type, paramters);

            EmailClient client = new EmailClient(_options.EmailServiceConfig.ConnectionString);

            var content = classifiedEmail.SelectEmailContent();
            var message = new EmailMessage(_options.EmailServiceConfig.Sender, email, content);

            await client.SendAsync(Azure.WaitUntil.Completed, message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(SendAsync));
            throw;
        }
    }

    public Dictionary<string, string> GenerateEmailConfirmationParameters(User user, string token)
    {
        try
        {
            return new Dictionary<string, string>()
            {
                { "fullName", $"{user.FirstName} {user.LastName}" },
                { "url", $"{_options.FrontendConfig.Url}/verify?email={user.Email}&token={token}" }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(GenerateEmailConfirmationParameters));
            throw;
        }
    }

    public Dictionary<string, string> GenerateResetPasswordParameters(User user, string token)
    {
        try
        {
            return new Dictionary<string, string>()
            {
                { "fullName", $"{user.FirstName} {user.LastName}" },
                {
                    "url",
                    $"{_options.FrontendConfig.Url}/reset-password?email={user.Email}&token={token}"
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(GenerateResetPasswordParameters));
            throw;
        }
    }
}
