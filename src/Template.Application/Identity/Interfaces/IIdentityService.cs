using Template.Application.Identity.Commands.CreateUser;
using Template.Application.Identity.Commands.VerifyEmail;
using Template.Application.Identity.Common;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Entites;

namespace Template.Application.Identity.Interfaces;

public interface IIdentityService
{
    Task<Result<User>> FindUserAsync(FindUserDto request);
    Task<Result<object>> CreateUserAsync(CreateUserDto request);
    Task<Result<string>> GenerateEmailVerificationTokenAsync(string email);
    Task<Result<object>> VerifyEmailAsync(VerifyEmailDto request);
    Task<Result<string>> GenerateResetPasswordTokenAsync(string email);
}
