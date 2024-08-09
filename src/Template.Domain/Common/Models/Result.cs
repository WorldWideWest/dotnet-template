namespace Template.Domain.Common.Models
{
    /// <summary>
    /// Represents the result of an operation, containing data, additional info, and potential errors.
    /// </summary>
    /// <typeparam name="TData">The type of data returned by the operation.</typeparam>
    /// <typeparam name="TInfo">The type of additional info related to the operation.</typeparam>
    public class Result<TData, TInfo>
        where TData : class
        where TInfo : class
    {
        private static readonly Result<TData, TInfo> _success = new Result<TData, TInfo>()
        {
            Succeeded = true
        };
        private readonly List<Error> _errors = new List<Error>();

        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool Succeeded { get; init; }

        /// <summary>
        /// Gets the data returned by the operation, if any.
        /// </summary>
        public TData Data { get; init; }

        /// <summary>
        /// Gets the additional info related to the operation, if any.
        /// </summary>
        public TInfo? Info { get; init; }

        /// <summary>
        /// Gets a collection of errors, if the operation failed.
        /// </summary>
        public IEnumerable<Error> Errors => _errors;

        /// <summary>
        /// Returns a successful result without any data or info.
        /// </summary>
        /// <returns>A successful <see cref="Result{TData, TInfo}"/> instance.</returns>
        public static Result<TData, TInfo> Success() => _success;

        /// <summary>
        /// Returns a successful result with the specified data.
        /// </summary>
        /// <param name="result">The data to be returned.</param>
        /// <returns>A successful <see cref="Result{TData, TInfo}"/> instance with the specified data.</returns>
        public static Result<TData, TInfo> Success(TData result, TInfo info = null) =>
            new Result<TData, TInfo>()
            {
                Succeeded = true,
                Data = result,
                Info = info
            };

        /// <summary>
        /// Returns a failed result with the specified errors.
        /// </summary>
        /// <param name="errors">An array of <see cref="Error"/> objects describing the failure.</param>
        /// <returns>A failed <see cref="Result{TData, TInfo}"/> instance with the specified errors.</returns>
        public static Result<TData, TInfo> Failed(params Error[] errors)
        {
            var result = new Result<TData, TInfo>() { Succeeded = false };

            if (errors is not null)
                result._errors.AddRange(errors);

            return result;
        }

        /// <summary>
        /// Returns a failed result with a specific error code and description.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <returns>A failed <see cref="Result{TData, TInfo}"/> instance with the specified error.</returns>
        public static Result<TData, TInfo> Failed(string code, string description)
        {
            var error = new Error(code, description);

            var result = new Result<TData, TInfo>() { Succeeded = false };

            if (error is not null)
                result._errors.Add(error);

            return result;
        }
    }
}
