
using MediatR;
using FluentValidation;
using System.Collections.Immutable;

namespace Medicine.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators == null || !_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = _validators.Select(async v => await v.ValidateAsync(context, cancellationToken))
                   .Select(task => task.Result)
                   .SelectMany(v => v.Errors)
                   .Where(v => v != null)
                   .ToImmutableList();

        if (failures.IsEmpty)
            return await next();

        throw new ValidationException(failures);
    }
}