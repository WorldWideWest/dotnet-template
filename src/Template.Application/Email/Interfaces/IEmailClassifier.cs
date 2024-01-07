using Template.Domain.Email.Enums;
using Template.Domain.Email.Models;

namespace Template.Application.Email.Interfaces;

public interface IEmailClassifier
{
    bool Classified(EmailType type);
    ClassifiedEmail GetEmail(IDictionary<string, string> parameters);
}
