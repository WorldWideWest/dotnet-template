using Template.Api.Extensions;
using Template.Api.Extensions.DependencyInjection;
using Template.Application.Extensions.DependencyInjection;
using Template.Infrastructure.Extensions;
using Template.Infrastructure.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.Internal.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddApi().AddInfrastructure(builder.Configuration).AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await IdentityServerMigrationExtension.ApplyMigrationsAsync(app);

    app.UseDeveloperExceptionPage();

    app.UseSwaggerExtension();
}

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});

app.UseForwardedHeadersExtension();

app.UseExceptionHandler();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
