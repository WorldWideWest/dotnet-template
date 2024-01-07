using Azure.Communication.Email;

namespace Template.Domain.Email.Models;

public class ClassifiedEmail
{
    public string Subject { get; set; }
    public string Body { get; set; }

    public static EmailContent GetEmailContent(ClassifiedEmail email) =>
        new EmailContent(email.Subject) { Html = email.Body };
}
