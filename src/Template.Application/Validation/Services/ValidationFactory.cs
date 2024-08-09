using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Validation.Extensions;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Application.Validation.Services;

public class ValidationFactory : IValidationFactory
{
    private readonly IServiceProvider _provider;

    public ValidationFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Result<object, object>> ValidateAsync<T>(T request)
    {
        var validator = _provider.GetService<IValidator<T>>();

        if (validator is null)
            return Result<object, object>.Success();

        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
            return Result<object, object>.Failed(result.ToErrors());

        return Result<object, object>.Success();
    }
}
