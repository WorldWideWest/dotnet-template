using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Identity.Commands.CreateUser;
using Template.Application.Identity.Commands.ResendConfirmationEmail;
using Template.Application.Identity.Commands.VerifyEmail;
using Template.Domain.Common.Models;

namespace Template.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class IdentityController(ILogger<IdentityController> logger, IMediator mediator)
    : ControllerBase
{
    private readonly ILogger<IdentityController> _logger = logger;
    private readonly IMediator _mediator = mediator;

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<object>>> CreateUserAsnyc(
        [FromBody] RegisterUserCommand request
    )
    {
        try
        {
            var result = await _mediator.Send(request);
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
    public async Task<ActionResult<Result<object>>> VerifyEmailAsnyc(
        [FromBody] VerifyEmailCommand request
    )
    {
        try
        {
            var result = await _mediator.Send(request);
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
    public async Task<ActionResult<Result<object>>> ResendVerificationEmailAsnyc(
        [FromBody] ResendConfirmationEmailCommand request
    )
    {
        try
        {
            var result = await _mediator.Send(request);
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
}
