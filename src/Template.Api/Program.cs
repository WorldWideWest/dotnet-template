using Template.Application;
using Template.Infrastructure;
using Template.Infrastructure.IdentityServer.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false)
    .AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration).AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await IdentityServerMigrationConfiguration.ApplyMigrationsAsync(app);
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.Run();
