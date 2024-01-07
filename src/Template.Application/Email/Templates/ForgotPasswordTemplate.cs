using Template.Application.Email.Interfaces;
using Template.Domain.Email.Constants;
using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;

namespace Template.Application.Email.Templates;

public class ForgotPasswordTemplate : IEmailClassifier
{
    public bool Classified(EmailType type) => EmailType.PasswordReset == type;

    public ClassifiedEmail GetEmail(IDictionary<string, string> bodyParameters)
    {
        ClassifiedEmail classified =
            new()
            {
                Subject = EmailSubject.PasswordReset,
                Body = EmailBody.PasswordReset(bodyParameters["fullName"], bodyParameters["url"])
            };

        return classified;
    }
}
