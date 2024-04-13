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
    Task<Result<User>> FindUserAsync(FindUserDto request);
    Task<Result<object>> CreateUserAsync(CreateUserRequest request);
    Task<Result<string>> GenerateEmailVerificationTokenAsync(string email);
    Task<Result<object>> VerifyEmailAsync(VerifyEmailRequest request);
    Task<Result<string>> GenerateResetPasswordTokenAsync(string email);
    Task<Result<object>> ResetPasswordAsync(ResetPasswordRequest request);
    Task<Result<object>> ChangePasswordAsync(ChangePasswordRequest request);
    Task<Result<object>> DeleteUserAsync(FindUserDto request);
    Task<Result<object>> RegisterExternalAsync(AuthenticateResult result);
}
