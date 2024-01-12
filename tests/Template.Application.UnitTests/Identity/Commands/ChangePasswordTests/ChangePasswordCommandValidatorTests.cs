using Template.Application.Identity.Commands.ChangePassword;

namespace Template.Application.UnitTests.Identity.Commands.ChangePasswordTests;

public partial class ChangePasswordCommandValidatorTests
{
    private readonly ChangePasswordCommandValidator _validator =
        new ChangePasswordCommandValidator();

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

public partial class ChangePasswordCommandValidatorTests
{
    public static ChangePasswordCommand ValidRequest => new("OldPass123#", "NewPass123#");

    public static ChangePasswordCommand InvalidRequest => new("", "");
}
