namespace Template.Domain.IdentityServer.Constants.Authorization;

public static class GrantType
{
    public const string Password = "password";

    public static IEnumerable<string> SupportedGrantTypes => new List<string> { Password };
}
