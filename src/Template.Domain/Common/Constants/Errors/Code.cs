namespace Template.Domain.Common.Constants.Errors;

public static class ErrorCode
{
    public static string PropertyError(string property) => $"ERR_{property}";
}
