using FluentAssertions;
using Template.Application.Identity.Commands.CreateUser;

namespace Template.Application.IntegrationTests.Identity.Commands.CreateUser;

public class CreateUserTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Create_Should_CreateNewUser()
    {
        var command = new CreateUserCommand(
            "Dzenan",
            "Dzafic",
            "dzeno.dzafic@gmail.com",
            "Eminem662#"
        );
        var result = await Mediator.Send(command);

        result.Succeeded.Should().BeTrue();
    }
}
