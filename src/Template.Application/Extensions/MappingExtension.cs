using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Common.Models;

namespace Template.Application.Extensions;

public static class MappingExtension
{
    public static Error[] ToError(this ValidationResult result)
    {
        if (!result.Errors.Any())
            return [];

        return result.Errors.Select(x => new Error(x.ErrorCode, x.ErrorMessage)).ToArray();
    }

    public static Error[] ToError(this IdentityResult result)
    {
        if (!result.Errors.Any())
            return [];

        return result.Errors.Select(x => new Error(x.Code, x.Description)).ToArray();
    }
}
