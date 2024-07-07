
using Easebnb.Application.Common.Validators;
using Easebnb.Domain.User;
using Easebnb.Domain.User.Services;
using Easebnb.Shared;
using ErrorOr;
using System.ComponentModel.DataAnnotations;

namespace Easebnb.Application.User.Commands;

public class RegisterUserCommand : ICommand<ErrorOr<Success>>
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string ConfirmPassword { get; set; } = null!;
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull().WithMessage("Username is required")
            .MinimumLength(4).WithMessage("Username must be at least 4 characters")
            .MaximumLength(255).WithMessage("Username must not exceed 255 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull().WithMessage("Email is required")
            .EmailAddress();

        RuleFor(x => x.Password)
            .Password()
            .Equal(x => x.ConfirmPassword).WithMessage("\"Password and\" \"Confirm Password\" must match");
    }
}

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ErrorOr<Success>>
{
    private readonly IUserService _userService;
    public RegisterUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<ErrorOr<Success>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = UserEntity.Create(request.UserName, request.Email);
        var result = await _userService.CreateUserAsync(user, request.Password);
        return result.Value != null ? Result.Success : result.Errors;
    }
}
