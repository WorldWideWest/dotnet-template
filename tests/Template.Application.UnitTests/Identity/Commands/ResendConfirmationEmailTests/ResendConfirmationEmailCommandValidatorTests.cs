using Template.Application.Identity.Commands.ResendConfirmationEmail;

namespace Template.Application.UnitTests.Identity.Commands.ResendConfirmationEmailTests;

public partial class ResendConfirmationEmailCommandValidatorTests
{
    private readonly ResendConfirmationEmailCommandValidator _validator =
        new ResendConfirmationEmailCommandValidator();

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

public partial class ResendConfirmationEmailCommandValidatorTests
{
    public static ResendConfirmationEmailCommand ValidRequest => new("dz.dz@something.net");

    public static ResendConfirmationEmailCommand InvalidRequest => new("dz.dzomething.net");
}
