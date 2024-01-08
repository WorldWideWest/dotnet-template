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

    public static IRuleBuilderOptions<T, string> AreScopesSupported<T>(
        this IRuleBuilder<T, string> ruleBuilder
    )
    {
        return ruleBuilder.Must(
            (rootObject, scope, context) =>
            {
                var scopes = scope.Split(" ");

                if (!scopes.Any())
                    return false;

                var isValid = scopes.All(x => ApiScope.SupportedApiScopes.Contains(x));

                return isValid;
            }
        );
    }
}
