using Template.Domain.Common.Models;

namespace Template.Application.Validation.Interfaces;

public interface IValidationFactory
{
    Task<Result<object>> ValidateAsync<T>(T request);
}
