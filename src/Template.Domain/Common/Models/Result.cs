namespace Template.Domain.Common.Models;

public class Result<TResponse>
    where TResponse : class
{
    private static readonly Result<TResponse> _success = new Result<TResponse>()
    {
        Succeeded = true
    };
    private readonly List<Error> _errors = new List<Error>();

    public bool Succeeded { get; protected set; }
    public TResponse Body { get; protected set; }
    public IEnumerable<Error> Errors => _errors;

    /// <summary>
    /// Set the Succeeded property to true, use it when you dont have to return any data inside the Body property
    /// </summary>
    /// <returns>Result of type <typeparamref name="TResponse"/></returns>
    public static Result<TResponse> Success() => _success;

    /// <summary>
    /// Use this overload when you have to return something to the caller, the input provided to this method will be set into the Body property and the Succeeded property will be set to true
    /// </summary>
    /// <param name="result">Object that will be provided to the Body Property</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Result of type <typeparamref name="TResponse"/></returns>
    public static Result<TResponse> Success(TResponse result) =>
        new Result<TResponse>() { Succeeded = true, Body = result };

    /// <summary>
    /// Provide the BaseError Array and return it, the Succeeded property will be set to false
    /// </summary>
    /// <param name="errors">Array of BaseError objects</param>
    /// <returns>Result of type <typeparamref name="TResponse"/></returns>
    public static Result<TResponse> Failed(params Error[] errors)
    {
        var result = new Result<TResponse>() { Succeeded = false };

        if (errors is not null)
            result._errors.AddRange(errors);

        return result;
    }

    /// <summary>
    /// Provide code and description of the error, the object will be assembled inside the method and then return it, the Succeeded property will be set to false
    /// </summary>
    /// <param name="code">Error Code</param>
    /// <param name="description">Error Description</param>
    /// <returns>Result of type <typeparamref name="TResponse"/></returns>
    public static Result<TResponse> Failed(string code, string description)
    {
        var error = new Error(code, description);

        var result = new Result<TResponse>() { Succeeded = false };

        if (error is not null)
            result._errors.Add(error);

        return result;
    }
}
