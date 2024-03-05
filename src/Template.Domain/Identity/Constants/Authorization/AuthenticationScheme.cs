using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Template.Domain.Identity.Constants.Authorization;

public static class AuthenticationScheme
{
    public const string DefaultAndGoogle =
        $"{JwtBearerDefaults.AuthenticationScheme},{GoogleDefaults.AuthenticationScheme}";
}
