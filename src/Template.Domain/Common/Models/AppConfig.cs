namespace Template.Domain.Common.Models;

public class AppConfig
{
    public IdentityServerConfig IdentityServerConfig { get; set; }
    public EmailServiceConfig EmailServiceConfig { get; set; }
    public FrontendConfig FrontendConfig { get; set; }
}

public class IdentityServerConfig
{
    public IdentityServerClients Clients { get; set; }
}

public class IdentityServerClients
{
    public InternalClient Web { get; set; }
    public InternalClient Mobile { get; set; }
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
    public required string RedirectUri { get; set; }
    public required string PostLogoutRedirectUri { get; set; }
    public required string AllowedCorsOrigin { get; set; }
}

public class EmailServiceConfig
{
    public required string ConnectionString { get; set; }
    public required string Sender { get; set; }
}

public class FrontendConfig
{
    public string Url { get; set; }
}
