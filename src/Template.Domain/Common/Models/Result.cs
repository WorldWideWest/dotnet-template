using Microsoft.AspNetCore.Identity;

namespace Template.Domain.Common.Models;

public class Result<T>
    where T : class
{
    private static readonly Result<T> _success = new Result<T>() { Succeeded = true };
    private readonly List<Error> _errors = new List<Error>();

    public bool Succeeded { get; protected set; }
    public T Body { get; protected set; }
    public IEnumerable<Error> Errors => _errors;

    /// <summary>
    /// Set the Succeeded property to true, use it when you dont have to return any data inside the Body property
    /// </summary>
    /// <returns>Result of type <typeparamref name="T"/></returns>
    public static Result<T> Success() => _success;

    /// <summary>
    /// Use this overload when you have to return something to the caller, the input provided to this method will be set into the Body property and the Succeeded property will be set to true
    /// </summary>
    /// <param name="result">Object that will be provided to the Body Property</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Result of type <typeparamref name="T"/></returns>
    public static Result<T> Success(T result) =>
        new Result<T>() { Succeeded = true, Body = result };

    /// <summary>
    /// Provide the BaseError Array and return it, the Succeeded property will be set to false
    /// </summary>
    /// <param name="errors">Array of BaseError objects</param>
    /// <returns>Result of type <typeparamref name="T"/></returns>
    public static Result<T> Failed(params Error[] errors)
    {
        var result = new Result<T>() { Succeeded = false };

        if (errors is not null)
            result._errors.AddRange(errors);

        return result;
    }

    /// <summary>
    /// Provide code and description of the error, the object will be assembled inside the method and then return it, the Succeeded property will be set to false
    /// </summary>
    /// <param name="code">Error Code</param>
    /// <param name="description">Error Description</param>
    /// <returns>Result of type <typeparamref name="T"/></returns>
    public static Result<T> Failed(string code, string description)
    {
        var error = new Error() { Code = code, Description = description };

        var result = new Result<T>() { Succeeded = false };

        if (error is not null)
            result._errors.Add(error);

        return result;
    }

    /// <summary>
    /// Mapping from identity error to BaseError and retuning the Result with the mapped errors
    /// </summary>
    /// <param name="errors"></param>
    /// <returns>Result of type <typeparamref name="T"/></returns>
    public static Result<T> Failed(params IdentityError[] errors)
    {
        var mappedErrors = errors.Select(
            x => new Error { Code = x.Code, Description = x.Description }
        );

        var result = new Result<T>() { Succeeded = false };

        if (mappedErrors is not null)
            result._errors.AddRange(mappedErrors);

        return result;
    }
}
