namespace Template.Domain.Common.Constants.Errors;

public static class ErrorMessage
{
    public static string PropertyError(string property) => $"{property} is not valid";

    public static string PropertyEmpty(string property) => $"{property} can't be empty";

    public static string PropertyLength(string property, int min, int max) =>
        $"{property} doesn't match the required length, the required length is between {min} and {max} characters";

    public static string InvalidCharacters(string property) =>
        $"{property} can not have special characters or numbers";
}
