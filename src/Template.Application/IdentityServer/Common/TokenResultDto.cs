namespace Template.Application.IdentityServer.Common;

public record TokenResultDto(string AccessToken, int ExpiresIn, string RefreshToken = null);
