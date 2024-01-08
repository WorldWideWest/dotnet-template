using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;
using Template.Domain.Identity.Entites;

namespace Template.Application.Email.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailType type, string email, Dictionary<string, string> paramters);
    ClassifiedEmail Classify(EmailType type, Dictionary<string, string> paramters);
    Dictionary<string, string> GenerateEmailConfirmationParameters(User user, string token);
    Dictionary<string, string> GenerateResetPasswordParameters(User user, string token);
}
