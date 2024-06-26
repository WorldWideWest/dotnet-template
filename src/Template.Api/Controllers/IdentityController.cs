using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Identity.Commands.ChangePassword;
using Template.Application.Identity.Commands.CreateUser;
using Template.Application.Identity.Commands.DeleteUser;
using Template.Application.Identity.Commands.ExternalSignIn;
using Template.Application.Identity.Commands.ForgotPassword;
using Template.Application.Identity.Commands.ResendConfirmationEmail;
using Template.Application.Identity.Commands.ResetPassword;
using Template.Application.Identity.Commands.VerifyEmail;
using Template.Application.Identity.Queries.GetProvider;
using Template.Domain.Common.Constants;
using Template.Domain.Common.Models;
using Template.Domain.Identity.Constants.Authorization;

namespace Template.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class IdentityController : ControllerBase
{
    private readonly ILogger<IdentityController> _logger;
    private readonly IMediator _mediator;

    public IdentityController(ILogger<IdentityController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object>>> CreateUserAsnyc(
        [FromBody] CreateUserCommand request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(CreateUserAsnyc));
            throw;
        }
    }

    [AllowAnonymous]
    [HttpPost("verify")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object>>> VerifyEmailAsnyc(
        [FromBody] VerifyEmailCommand request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(VerifyEmailAsnyc));
            throw;
        }
    }

    [AllowAnonymous]
    [HttpPost("verify/resend")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object>>> ResendVerificationEmailAsnyc(
        [FromBody] ResendConfirmationEmailCommand request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ResendVerificationEmailAsnyc));
            throw;
        }
    }

    [AllowAnonymous]
    [HttpPost("password/forgot")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object>>> ForgotPasswordAsync(
        [FromBody] ForgotPasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ForgotPasswordAsync));
            throw;
        }
    }

    [AllowAnonymous]
    [HttpPost("password/reset")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object>>> ResetPasswordAsync(
        [FromBody] ResetPasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ResetPasswordAsync));
            throw;
        }
    }

    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = Policy.UpdateProfilePasswordAccess
    )]
    [HttpPut("password/change")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object>>> ChangetPasswordAsync(
        [FromBody] ChangePasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            request.Email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ChangetPasswordAsync));
            throw;
        }
    }

    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = Policy.DeleteAccess
    )]
    [HttpDelete("delete")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object>>> DeleteUserAsync(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var request = new DeleteUserCommand(User.FindFirst(ClaimTypes.Email)?.Value);

            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(DeleteUserAsync));
            throw;
        }
    }

    [AllowAnonymous]
    [HttpGet(IdentityDefaults.LoginPath)]
    public async Task<IActionResult> Login(
        string returnUrl,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var request = new GetProviderQuery(returnUrl, Request);

            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Challenge(result.Body.Properties, result.Body.Provider);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Login));
            throw;
        }
    }

    [AllowAnonymous]
    [HttpGet(IdentityDefaults.CallbackPath)]
    public async Task<IActionResult> ExternalLoginCallback(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var request = new ExternalSignInCommand(HttpContext);

            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Redirect(result.Body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ExternalLoginCallback));
            throw;
        }
    }

    [AllowAnonymous]
    [HttpGet(IdentityDefaults.LogoutPath)]
    public async Task<IActionResult> Logout(
        string logoutId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var request = new ExternalSignOutCommand(logoutId);

            var result = await _mediator.Send(request, cancellationToken);

            if (!result.Succeeded)
                return BadRequest(result);

            return Redirect(result.Body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(Logout));
            throw;
        }
    }
}
