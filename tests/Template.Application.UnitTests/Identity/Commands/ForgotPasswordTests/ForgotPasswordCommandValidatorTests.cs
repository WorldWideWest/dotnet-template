using Template.Application.Identity.Commands.ForgotPassword;

namespace Template.Application.UnitTests.Identity.Commands.ForgotPasswordTests;

public partial class ForgotPasswordCommandValidatorTests
{
    private readonly ForgotPasswordCommandValidator _validator =
        new ForgotPasswordCommandValidator();

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

public partial class ForgotPasswordCommandValidatorTests
{
    public static ForgotPasswordCommand ValidRequest => new("dz.dz@something.net");

    public static ForgotPasswordCommand InvalidRequest => new("dz.dzomething.net");
}
