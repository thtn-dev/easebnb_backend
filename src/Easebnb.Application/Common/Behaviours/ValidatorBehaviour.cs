namespace Easebnb.Application.Common.Behaviours;
public class ValidatorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidatorBehaviour(IValidator<TRequest> validator)
    {
        _validator = validator;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var ctx = new ValidationContext<TRequest>(request);

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
           .ConvertAll(error => Error.Validation(
               code: error.PropertyName,
               description: error.ErrorMessage));

        return (dynamic)errors;
    }
}