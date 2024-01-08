using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Commands.CreateUser;
using Template.Application.Identity.Commands.VerifyEmail;
using Template.Application.Identity.Common;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Errors;
using Template.Domain.Identity.Entites;

namespace Template.Infrastructure.Identity.Services;

public sealed class IdentityService(
    ILogger<IdentityService> logger,
    UserManager<User> userManager,
    IPasswordHasher<User> passwordHasher
) : IIdentityService
{
    private readonly ILogger<IdentityService> _logger = logger;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task<Result<User>> FindUserAsync(FindUserDto request)
    {
        try
        {
            var result = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
            if (result is null)
                return Result<User>.Failed(ErrorCode.ERR_USER, ErrorMessage.USER_DOES_NOT_EXIST);

            return Result<User>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(FindUserAsync));
            throw;
        }
    }

    public async Task<Result<object>> CreateUserAsync(CreateUserDto request)
    {
        try
        {
            var searchResult = await FindUserAsync(new(request.Email));
            if (searchResult.Succeeded)
                return Result<object>.Failed(ErrorCode.ERR_USER, ErrorMessage.USER_ALREADY_EXISTS);

            var user = CreateUserDto.ToEntity(request);
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var result = await _userManager.CreateAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
                return Result<object>.Failed(result.Errors.ToArray());

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(CreateUserAsync));
            throw;
        }
    }

    public async Task<Result<string>> GenerateEmailVerificationTokenAsync(string email)
    {
        try
        {
            var searchResult = await FindUserAsync(new(email));
            if (!searchResult.Succeeded)
                return Result<string>.Failed(searchResult.Errors.ToArray());

            var token = await _userManager
                .GenerateEmailConfirmationTokenAsync(searchResult.Body)
                .ConfigureAwait(false);

            var result = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            return Result<string>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(FindUserAsync));
            throw;
        }
    }

    public async Task<Result<object>> VerifyEmailAsync(VerifyEmailDto request)
    {
        try
        {
            var token = Encoding.UTF8.GetString(Convert.FromBase64String(request.Token));

            var result = await _userManager
                .ConfirmEmailAsync(request.User, token)
                .ConfigureAwait(false);

            if (!result.Succeeded)
                return Result<object>.Failed(result.Errors.ToArray());

            var claimsResult = await _userManager
                .AddClaimsAsync(
                    request.User,
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Email, request.User.Email),
                        new Claim(JwtClaimTypes.GivenName, request.User.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, request.User.LastName)
                    }
                )
                .ConfigureAwait(false);

            if (!claimsResult.Succeeded)
                return Result<object>.Failed(claimsResult.Errors.ToArray());

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(VerifyEmailAsync));
            throw;
        }
    }
}
