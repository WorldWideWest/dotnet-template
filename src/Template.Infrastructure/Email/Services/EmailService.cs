using Template.Application.Email.Interfaces;
using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;

namespace Template.Infrastructure.Email.Services;

public class EmailService : IEmailService
{
    public ClassifiedEmail Classify(EmailType type, Dictionary<string, string> paramters)
    {
        throw new NotImplementedException();
    }

    public Task SendAsync(EmailType type, string email, Dictionary<string, string> paramters)
    {
        throw new NotImplementedException();
    }
}
