using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Template.Application.Validation.Interfaces;
using Template.Domain.Common.Models;

namespace Template.Infrastructure.Validation.Services;

public class ValidationFactory(ILogger<ValidationFactory> logger, IServiceProvider provider)
    : IValidationFactory
{
    private readonly ILogger<ValidationFactory> _logger = logger;
    private readonly IServiceProvider _provider = provider;

    public async Task<Result<object>> ValidateAsync<T>(T request)
    {
        try
        {
            var validator = _provider.GetService<IValidator<T>>();

            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return Result<object>.Failed(result.Errors.ToArray());

            return Result<object>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, nameof(ValidateAsync));
            throw;
        }
    }
}
