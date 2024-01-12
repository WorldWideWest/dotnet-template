using Template.Application.Identity.Commands.VerifyEmail;

namespace Template.Application.UnitTests.Identity.Commands.VerifyEmailTests;

public partial class VerifyEmailCommandValidatorTests
{
    private readonly VerifyEmailCommandValidator _validator = new VerifyEmailCommandValidator();

    [Fact]
    public async Task Validate_CreateUserCommand_ShuldReturnSuccess()
    {
        var result = await _validator.ValidateAsync(ValidRequest);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_CreateUserCommand_ShuldReturnFailure()
    {
        var result = await _validator.ValidateAsync(InvalidRequest);

        Assert.False(result.IsValid);
    }
}

public partial class VerifyEmailCommandValidatorTests
{
    public static VerifyEmailCommand ValidRequest =>
        new("dz.dz@something.net", Guid.NewGuid().ToString());

    public static VerifyEmailCommand InvalidRequest => new("dz.dzomething.net", "");
}
