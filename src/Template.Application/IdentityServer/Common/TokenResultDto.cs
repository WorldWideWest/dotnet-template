namespace Template.Application.IdentityServer.Common;

public record TokenResultDto(string AccessToken, int ExpiresAt, string RefreshToken = null);
