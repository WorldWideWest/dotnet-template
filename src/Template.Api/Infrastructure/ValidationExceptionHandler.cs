using Microsoft.AspNetCore.Diagnostics;
using Template.Application.Common.Exceptions;

namespace Template.Api.Infrastructure;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ValidationException validationException)
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(validationException.Result, cancellationToken);

        return true;
    }
}
