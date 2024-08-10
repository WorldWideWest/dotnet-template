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
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object, object>>> CreateUserAsnyc(
        [FromBody] CreateUserCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("verify")]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object, object>>> VerifyEmailAsnyc(
        [FromBody] VerifyEmailCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("verify/resend")]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object, object>>> ResendVerificationEmailAsnyc(
        [FromBody] ResendConfirmationEmailCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("password/forgot")]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object, object>>> ForgotPasswordAsync(
        [FromBody] ForgotPasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("password/reset")]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object, object>>> ResetPasswordAsync(
        [FromBody] ResetPasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }

    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = Policy.UpdateProfilePasswordAccess
    )]
    [HttpPut("password/change")]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object, object>>> ChangetPasswordAsync(
        [FromBody] ChangePasswordCommand request,
        CancellationToken cancellationToken = default
    )
    {
        request.Email = User.FindFirst(ClaimTypes.Email)?.Value;

        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }

    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = Policy.DeleteAccess
    )]
    [HttpDelete("delete")]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<object, object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result<object, object>>> DeleteUserAsync(
        CancellationToken cancellationToken = default
    )
    {
        var request = new DeleteUserCommand(User.FindFirst(ClaimTypes.Email)?.Value);

        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet(IdentityDefaults.LoginPath)]
    public async Task<IActionResult> Login(
        string returnUrl,
        CancellationToken cancellationToken = default
    )
    {
        var request = new GetProviderQuery(returnUrl, Request);

        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Challenge(result.Data.Properties, result.Data.Provider);
    }

    [AllowAnonymous]
    [HttpGet(IdentityDefaults.CallbackPath)]
    public async Task<IActionResult> ExternalLoginCallback(
        CancellationToken cancellationToken = default
    )
    {
        var request = new ExternalSignInCommand(HttpContext);

        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Redirect(result.Data);
    }

    [AllowAnonymous]
    [HttpGet(IdentityDefaults.LogoutPath)]
    public async Task<IActionResult> Logout(
        string logoutId,
        CancellationToken cancellationToken = default
    )
    {
        var request = new ExternalSignOutCommand(logoutId);

        var result = await _mediator.Send(request, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result);

        return Redirect(result.Data);
    }
}
