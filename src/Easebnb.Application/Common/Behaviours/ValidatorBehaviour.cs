using FluentValidation;
using MediatR;
using System.Net;

namespace Easebnb.Application.Common.Behaviours;
public class ValidatorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var ctx = new ValidationContext<TRequest>(request);

        var tasks = _validators.Select(v => v.ValidateAsync(ctx, cancellationToken));
        var results = await Task.WhenAll(tasks);
        var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count == 0) return await next();

        var messages = failures.Select(x => x.ErrorMessage).ToList();
        //throw new HttpException(HttpStatusCode.BadRequest, messages);
        throw new ValidationException(messages.First());

    }
}