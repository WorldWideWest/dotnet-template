using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Template.Application.Email.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;

namespace Template.Infrastructure.Email.Services;

public class EmailService(
    ILogger<EmailService> logger,
    IOptions<AppConfig> options,
    List<IEmailClassifier> classifier
) : IEmailService
{
    private readonly ILogger<EmailService> _logger = logger;
    private readonly AppConfig _options = options.Value;
    private readonly List<IEmailClassifier> _classifier = classifier;

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

            var content = ClassifiedEmail.GetEmailContent(classifiedEmail);
            var message = new EmailMessage(_options.EmailServiceConfig.Sender, email, content);

            await client.SendAsync(Azure.WaitUntil.Completed, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(SendAsync));
            throw;
        }
    }
}
