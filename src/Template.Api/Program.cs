using Template.Api;
using Template.Api.Extensions;
using Template.Application;
using Template.Infrastructure;
using Template.Infrastructure.IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration.SetBasePath(builder.Environment.ContentRootPath)
    // * OPTIONAL set to true due to latter introduction to ConfigMap
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddApi().AddInfrastructure(builder.Configuration).AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await IdentityServerMigrationExtension.ApplyMigrationsAsync(app);
    app.UseDeveloperExceptionPage();
    app.UseSwaggerConfiguration();
}

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});

app.UseForwardedHeadersConfiguration();

app.UseExceptionHandler();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
