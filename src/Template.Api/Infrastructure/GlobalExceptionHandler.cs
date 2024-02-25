using Microsoft.AspNetCore.Diagnostics;
using Template.Domain.Common.Constants.Errors;
using Template.Domain.Common.Models;

namespace Template.Api.Infrastructure;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        _logger.LogError(exception, exception.Message, nameof(TryHandleAsync));

        var error = new Error
        {
            Code = ErrorCode.INTERNAL_SERVER_ERROR,
            Description = ErrorMessage.INTERNAL_SERVER_ERROR
        };

        var result = Result<object>.Failed(error);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}
