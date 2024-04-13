using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Template.Application.Identity.Commands.ChangePassword;
using Template.Application.Identity.Commands.CreateUser;
using Template.Application.Identity.Commands.ResetPassword;
using Template.Application.Identity.Commands.VerifyEmail;
using Template.Application.Identity.Common;
using Template.Application.Identity.Extensions;
using Template.Application.Identity.Interfaces;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Errors;
using Template.Domain.Identity.Entites;

namespace Template.Infrastructure.Identity.Services;

public sealed class IdentityService : IIdentityService
{
    private readonly ILogger<IdentityService> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly SignInManager<User> _signInManager;

    public IdentityService(
        ILogger<IdentityService> logger,
        UserManager<User> userManager,
        IPasswordHasher<User> passwordHasher,
        SignInManager<User> signInManager
    )
    {
        _logger = logger;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _signInManager = signInManager;
    }

    public async Task<Result<User>> FindUserAsync(FindUserDto request)
    {
        try
        {
            var result = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
            if (result is null)
                return Result<User>.Failed(
                    ErrorCode.UserDoesNotExist,
                    ErrorMessage.UserDoesNotExist
                );

            return Result<User>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(FindUserAsync));
            throw;
        }
    }

    public async Task<Result<object>> CreateUserAsync(CreateUserRequest request)
    {
        try
        {
            var searchResult = await FindUserAsync(new(request.Email));
            if (searchResult.Succeeded)
                return Result<object>.Failed(
                    ErrorCode.UserAlreadyExists,
                    ErrorMessage.UserAlreadyExists
                );

            var user = request.ToEntity();
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var result = await _userManager.CreateAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
                return Result<object>.Failed(result.ToErrors());

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

    public async Task<Result<object>> VerifyEmailAsync(VerifyEmailRequest request)
    {
        try
        {
            var searchResult = await FindUserAsync(new(request.Email));
            if (!searchResult.Succeeded)
                return Result<object>.Failed(searchResult.Errors.ToArray());

            var token = Encoding.UTF8.GetString(Convert.FromBase64String(request.Token));

            var result = await _userManager
                .ConfirmEmailAsync(searchResult.Body, token)
                .ConfigureAwait(false);

            if (!result.Succeeded)
                return Result<object>.Failed(result.ToErrors());

            var claimsResult = await _userManager
                .AddClaimsAsync(searchResult.Body, searchResult.Body.SelectClaims())
                .ConfigureAwait(false);

            if (!claimsResult.Succeeded)
                return Result<object>.Failed(result.ToErrors());

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(VerifyEmailAsync));
            throw;
        }
    }

    public async Task<Result<string>> GenerateResetPasswordTokenAsync(string email)
    {
        try
        {
            var searchResult = await FindUserAsync(new(email));
            if (!searchResult.Succeeded)
                return Result<string>.Failed(searchResult.Errors.ToArray());

            var token = await _userManager
                .GeneratePasswordResetTokenAsync(searchResult.Body)
                .ConfigureAwait(false);

            var result = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            return Result<string>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(GenerateResetPasswordTokenAsync));
            throw;
        }
    }

    public async Task<Result<object>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        try
        {
            var searchResult = await FindUserAsync(new(request.Email));
            if (!searchResult.Succeeded)
                return Result<object>.Failed(searchResult.Errors.ToArray());

            var token = Encoding.UTF8.GetString(Convert.FromBase64String(request.Token));

            var result = await _userManager
                .ResetPasswordAsync(searchResult.Body, token, request.Password)
                .ConfigureAwait(false);

            if (!result.Succeeded)
                return Result<object>.Failed(result.ToErrors());

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ResetPasswordAsync));
            throw;
        }
    }

    public async Task<Result<object>> ChangePasswordAsync(ChangePasswordRequest request)
    {
        try
        {
            var searchResult = await FindUserAsync(new(request.Email));
            if (!searchResult.Succeeded)
                return Result<object>.Failed(searchResult.Errors.ToArray());

            var isOldPasswordCorrect = await _userManager
                .CheckPasswordAsync(searchResult.Body, request.OldPassword)
                .ConfigureAwait(false);

            if (!isOldPasswordCorrect)
                return Result<object>.Failed(
                    ErrorCode.InvalidPassword,
                    ErrorMessage.InvalidPassword
                );

            var user = searchResult.Body;
            user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);

            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
                return Result<object>.Failed(result.ToErrors());

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ChangePasswordAsync));
            throw;
        }
    }

    public async Task<Result<object>> DeleteUserAsync(FindUserDto request)
    {
        try
        {
            var userSearchResult = await FindUserAsync(new(request.Email));
            if (!userSearchResult.Succeeded)
                return Result<object>.Failed(userSearchResult.Errors.ToArray());

            var user = userSearchResult.Body;

            var result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
                return Result<object>.Failed(result.ToErrors());

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(DeleteUserAsync));
            throw;
        }
    }

    public async Task<Result<object>> RegisterExternalAsync(AuthenticateResult result)
    {
        try
        {
            var provider = result.SelectIdentityProvider();

            var userId = result.SelectUserId();
            var user = await _userManager.FindByLoginAsync(provider, userId).ConfigureAwait(false);

            if (user is null)
            {
                user = result.Principal.ToEntity();
                var userResult = await _userManager.CreateAsync(user).ConfigureAwait(false);

                if (!userResult.Succeeded)
                    return Result<object>.Failed(userResult.ToErrors());

                var claimsResult = await _userManager
                    .AddClaimsAsync(user, user.SelectClaims(provider))
                    .ConfigureAwait(false);

                if (!claimsResult.Succeeded)
                    return Result<object>.Failed(claimsResult.ToErrors());
            }

            var info = new UserLoginInfo(provider, userId, provider);

            var addLogin = _userManager.AddLoginAsync(user, info);
            var signIn = _signInManager.SignInAsync(user, isPersistent: false);

            await Task.WhenAll(addLogin, signIn).ConfigureAwait(false);

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(RegisterExternalAsync));
            throw;
        }
    }
}
