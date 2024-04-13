namespace Template.Domain.Common.Constants;

public static class IdentityDefaults
{
    public const string LoginPath = "external/login";
    public const string LogoutPath = "external/logout";
    public const string CallbackPath = "external/callback";
    public const string LoginUrl = $"/api/Identity/{LoginPath}";
    public const string LogoutUrl = $"/api/Identity/{LogoutPath}";
    public const string CallbackUrl = $"/api/Identity/{CallbackPath}";
}
