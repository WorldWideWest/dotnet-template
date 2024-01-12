using Template.Application.Identity.Commands.ResetPassword;

namespace Template.Application.UnitTests.Identity.Commands.ResetPasswordTests;

public partial class ResetPasswordCommandValidatorTests
{
    private readonly ResetPasswordCommandValidator _validator = new ResetPasswordCommandValidator();

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

public partial class ResetPasswordCommandValidatorTests
{
    public static ResetPasswordCommand ValidRequest =>
        new("dz.dz@something.net", Guid.NewGuid().ToString(), "Pass123#");

    public static ResetPasswordCommand InvalidRequest => new("dz.dzomething.net", "", "Pass123");
}
