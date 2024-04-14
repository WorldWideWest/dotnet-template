using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Template.Application.IntegrationTests.Extensions.Containers;
using Template.Infrastructure.Data;
using Testcontainers.MsSql;

namespace Template.Application.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithName("sql")
        .WithPassword(MsSqlServerExtension.Password)
        .WithImage(MsSqlServerExtension.SqlArm64Image)
        .WithPortBinding(1433)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<IdentityDbContext>));
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(MsSqlServerExtension.GetConnectionString());
            });

            services.RemoveAll(typeof(DbContextOptions<PersistedGrantDbContext>));
            services.AddDbContext<PersistedGrantDbContext>(options =>
            {
                options.UseSqlServer(MsSqlServerExtension.GetConnectionString());
            });

            services.RemoveAll(typeof(DbContextOptions<ConfigurationDbContext>));
            services.AddDbContext<ConfigurationDbContext>(options =>
            {
                options.UseSqlServer(MsSqlServerExtension.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _dbContainer.ExecScriptAsync($"CREATE DATABASE {MsSqlServerExtension.DatabaseName}");
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
