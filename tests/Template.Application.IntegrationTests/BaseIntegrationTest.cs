using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly IMediator Mediator;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
