using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;

namespace Template.Application.Email.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailType type, string email, Dictionary<string, string> paramters);
    ClassifiedEmail Classify(EmailType type, Dictionary<string, string> paramters);
}
