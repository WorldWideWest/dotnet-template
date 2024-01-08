namespace Template.Domain.Identity.Constants.Regex;

public static class Pattern
{
    public const string OnlyAlphabeticCharactersWithApostrophie = "^[a-zA-Z'_ ]*$";
    public const string Email = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
    public const string Password = @"^(?=.*\d)(?=.*[a-zA-Z])(?=.*\W)(?=.*[a-zA-Z]).{0,}$";
}
