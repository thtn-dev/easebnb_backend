
using Easebnb.Application.Common.Interfaces;
using Easebnb.Application.Common.Validators;
using Easebnb.Domain.User;
using Easebnb.Shared;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Easebnb.Application.User.Commands;

public class RegisterUserCommand : ICommand<ErrorOr<Success>>
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
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
    private readonly UserManager<UserEntity> _userManager;
    public RegisterUserCommandHandler(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ErrorOr<Success>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new UserEntity
        {
            UserName = request.UserName,
            Email = request.Email
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        return result.Succeeded ? Result.Success : Error.Validation(description: result.Errors.First().Description);
    }
}
