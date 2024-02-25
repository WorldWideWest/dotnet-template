namespace Template.Domain.Common.Constants.Errors;

public static class ErrorCode
{
    public static string PropertyError(string property) => $"ERR_{property}";

    public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
}
