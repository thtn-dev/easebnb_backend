using Ardalis.GuardClauses;
using Easebnb.Application.User.Dtos;
using Easebnb.Domain.Common.Options;
using Easebnb.Domain.User;
using Easebnb.Domain.User.Services;
using ErrorOr;
using Microsoft.IdentityModel.JsonWebTokens;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Easebnb.Application.User.Queries;

public class LoginQuery : IRequest<ErrorOr<UserLoginResultDto>>
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull().WithMessage("UserName is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull().WithMessage("Password is required");
    }
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<UserLoginResultDto>>
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly JwtSetting _jwtSetting;
    public LoginQueryHandler(IUserService userService, IJwtService jwtService, JwtSetting jwtSetting)
    {
        _jwtService = jwtService;
        _userService = userService;
        _jwtSetting = jwtSetting;
    }
    public async Task<ErrorOr<UserLoginResultDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _userService.SignInAsync(request.UserName, request.Password);
        if (!result.IsError)
        {
            var user = result.Value;
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Email, user.Email),
                new (JwtRegisteredClaimNames.UniqueName, user.UserName),
                new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Sub, user.Id)
            };

            var token = await _jwtService.GenerateJwtTokenAsync(claims);
            var data = new UserLoginResultDto
            {
                Token = token,
                ExpiresIn = _jwtSetting.ExpiryMinutes,
                Sub = user.Id
            };
            return data;
        }
        return Error.Validation(description: "Invalid email or password");
    }
}