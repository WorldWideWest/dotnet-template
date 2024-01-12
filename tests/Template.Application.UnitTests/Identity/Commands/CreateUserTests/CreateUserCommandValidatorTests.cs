using Template.Application.Identity.Commands.CreateUser;

namespace Template.Application.UnitTests.Identity.Commands.CreateUserTests;

public partial class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator = new CreateUserCommandValidator();

    [Fact]
    public async Task Validate_CreateUserCommand_ShuldReturnSuccess()
    {
        var result = await _validator.ValidateAsync(ValidUser);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_CreateUserCommand_ShuldReturnFailure()
    {
        var result = await _validator.ValidateAsync(InvalidUser);

        Assert.False(result.IsValid);
    }
}

public partial class CreateUserCommandValidatorTests
{
    public static CreateUserCommand ValidUser =>
        new("Dzenan", "Dzafic", "dz.dz@something.net", "Pass123#");

    public static CreateUserCommand InvalidUser =>
        new("Dzenan2", "1Dzafic", "dz.dzsomething.net", "sadsadsa");
}
