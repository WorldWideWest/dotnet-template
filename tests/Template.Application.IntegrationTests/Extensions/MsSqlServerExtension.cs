using Testcontainers.MsSql;

namespace Template.Application.IntegrationTests.Extensions.Containers;

public static class MsSqlServerExtension
{
    public const string User = "sa";
    public const string Password = "6ha9hqOpaJI430J*JVr*";
    public const string DatabaseName = "IdentityDb";
    public const string SqlArm64Image = "mcr.microsoft.com/azure-sql-edge:latest";
    public const string SqlAmd64Image = "mcr.microsoft.com/mssql/server:2022-latest";

    public static MsSqlConfiguration Configuration =>
        new MsSqlConfiguration(DatabaseName, User, Password);

    public static MsSqlContainer CreateInstance => new MsSqlContainer(Configuration);

    public static string GetConnectionString()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>
        {
            { "Server", "sql,1433" },
            { "Database", Configuration.Database },
            { "User Id", Configuration.Username },
            { "Password", Configuration.Password },
            { "TrustServerCertificate", bool.TrueString }
        };

        return string.Join(
            ";",
            dictionary.Select(
                (KeyValuePair<string, string> property) =>
                    string.Join("=", property.Key, property.Value)
            )
        );
    }
}
