using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Common.Exceptions;
using Template.Application.Validation.Interfaces;

namespace Template.Application.Common.Behaviours;

class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
    private readonly IValidationFactory _validationFactory;

    public ValidationBehaviour(
        ILogger<ValidationBehaviour<TRequest, TResponse>> logger,
        IValidationFactory validationFactory
    )
    {
        _logger = logger;
        _validationFactory = validationFactory;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var result = await _validationFactory.ValidateAsync(request);

        if (!result.Succeeded)
            throw new ValidationException(result);

        return await next();
    }
}
