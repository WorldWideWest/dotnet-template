using FluentValidation.Results;
using Template.Domain.Common.Models;

namespace Template.Application.Validation.Extensions;

public static class MappingExtension
{
    /// <summary>
    /// Converts a <see cref="ValidationResult"/> to an array of <see cref="Error"/> objects.
    /// </summary>
    /// <param name="result">The <see cref="ValidationResult"/> to convert.</param>
    /// <returns>An array of <see cref="Error"/> objects.</returns>
    public static Error[] ToErrors(this ValidationResult result)
    {
        if (!result.Errors.Any())
            return [];

        return result.Errors.Select(x => new Error(x.ErrorCode, x.ErrorMessage)).ToArray();
    }
}
