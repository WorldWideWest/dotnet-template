namespace Template.Domain.IdentityServer.Constants.Authorization;

public static class ApiScope
{
    public const string Read = $"{ApiResource.Template}.read";
    public const string Write = $"{ApiResource.Template}.write";
    public const string Update = $"{ApiResource.Template}.update";
    public const string Delete = $"{ApiResource.Template}.delete";
    public const string Test = $"{ApiResource.Template}.test";

    public static IEnumerable<string> SupportedApiScopes =>
        new List<string> { Read, Write, Update, Delete };
}
