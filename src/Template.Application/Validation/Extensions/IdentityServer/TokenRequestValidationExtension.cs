using FluentValidation;
using Template.Domain.IdentityServer.Constants.Authorization;

namespace Template.Application.Validation.Extensions.IdentityServer;

public static class TokenRequestValidationExtension
{
    public static IRuleBuilderOptions<T, string> IsGrantTypeSupported<T>(
        this IRuleBuilder<T, string> ruleBuilder
    )
    {
        return ruleBuilder.Must(x => GrantType.SupportedGrantTypes.Contains(x));
    }
}
