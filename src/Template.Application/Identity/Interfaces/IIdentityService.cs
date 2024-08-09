using Microsoft.AspNetCore.Authentication;
using Template.Application.Identity.Commands.ChangePassword;
using Template.Application.Identity.Commands.CreateUser;
using Template.Application.Identity.Commands.ResetPassword;
using Template.Application.Identity.Commands.VerifyEmail;
using Template.Application.Identity.Common;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;

namespace Template.Application.Identity.Interfaces;

public interface IIdentityService
{
    Task<Result<User, object>> FindUserAsync(FindUserDto request);
    Task<Result<object, object>> CreateUserAsync(CreateUserRequest request);
    Task<Result<string, object>> GenerateEmailVerificationTokenAsync(string email);
    Task<Result<object, object>> VerifyEmailAsync(VerifyEmailRequest request);
    Task<Result<string, object>> GenerateResetPasswordTokenAsync(string email);
    Task<Result<object, object>> ResetPasswordAsync(ResetPasswordRequest request);
    Task<Result<object, object>> ChangePasswordAsync(ChangePasswordRequest request);
    Task<Result<object, object>> DeleteUserAsync(FindUserDto request);
    Task<Result<object, object>> RegisterExternalAsync(AuthenticateResult result);
}
