using Template.Api;
using Template.Api.Configurations;
using Template.Application;
using Template.Infrastructure;
using Template.Infrastructure.IdentityServer.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false)
    .AddEnvironmentVariables();

builder.Services.AddApi().AddInfrastructure(builder.Configuration).AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await IdentityServerMigrationConfiguration.ApplyMigrationsAsync(app);
    app.UseDeveloperExceptionPage();
    app.UseSwaggerConfiguration();
}

app.UseForwardedHeadersConfiguration();

app.UseExceptionHandler();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
