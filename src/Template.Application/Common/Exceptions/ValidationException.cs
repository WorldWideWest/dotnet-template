using Template.Domain.Common.Models;

namespace Template.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base() { }

    public ValidationException(Result<object, object> result)
    {
        Result = result;
    }

    public Result<object, object> Result { get; }
}
