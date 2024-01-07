using Template.Application.Email.Interfaces;
using Template.Domain.Email.Constants;
using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;

namespace Template.Application.Email.Templates;

public class EmailVerificationTemplate : IEmailClassifier
{
    public bool Classified(EmailType type) => EmailType.Verification == type;

    public ClassifiedEmail GetEmail(IDictionary<string, string> parameters)
    {
        ClassifiedEmail classified =
            new()
            {
                Subject = EmailSubject.Verification,
                Body = EmailBody.Verification(parameters["fullName"], parameters["url"])
            };

        return classified;
    }
}
