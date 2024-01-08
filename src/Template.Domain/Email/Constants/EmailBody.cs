namespace Template.Domain.Email.Constants;

public static class EmailBody
{
    public static string Verification(string fullName, string url)
    {
        return @$"
                <p>Dear {fullName},</p>
                <p>Congratulations and welcome to Template! This email serves as confirmation that your registration has been successfully completed.  We're thrilled to have you as a part of our community and look forward to providing you with a rewarding experience.</p>
                <a href={url}>Complete the Registration process by clicking the link</a>
                <br/>
                <p>Best Regards,</p>
                <p>Your Template Team</p>";
    }

    public static string PasswordReset(string fullName, string url)
    {
        return @$"
                <p>Dear {fullName},</p>
                <p>We have received a request to reset your password for your account. To ensure the security of your account, please follow the instructions below:</p>
                <ul>
                    <li>Click on the following link to <a href={url}>reset your password</a></li>
                    <li>You will be redirected to a secure page where you can enter your new password</li>
                    <li>Choose a strong, unique password that you haven't used before and is not easily guessable.</li>
                </ul>
                <p>Best Regards,</p>
                <p>Your Template Team</p>";
    }
}
