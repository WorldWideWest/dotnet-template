namespace Template.Domain.Common.Models;

public class AppConfig
{
    public IdentityServerConfig IdentityServerConfig { get; set; }
}

public class IdentityServerConfig
{
    public InternalClient Web { get; set; }
    public InternalClient Mobile { get; set; }
    public ExternalClient Google { get; set; }
}

public class InternalClient
{
    public required string Secret { get; set; }
}

public class ExternalClient
{
    public required string InternalSecret { get; set; }
    public required string ExternalClientId { get; set; }
    public required string ExternalClientSecret { get; set; }
    public string RedirectUri { get; set; }
    public string PostLogoutRedirectUri { get; set; }
    public string AllowedCorsOrigin { get; set; }
}
