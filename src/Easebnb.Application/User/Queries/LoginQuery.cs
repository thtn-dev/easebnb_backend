using Easebnb.Application.User.Dtos;
using Easebnb.Domain.Common.Options;
using Easebnb.Domain.Common.Services;
using Easebnb.Domain.User;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Easebnb.Application.User.Queries;

public class LoginQuery : IRequest<ErrorOr<UserLoginResultDto>>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
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
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IJwtService _jwtService;
    private readonly JwtSetting _jwtSetting;
    public LoginQueryHandler(SignInManager<UserEntity> signInManager, IJwtService jwtService, UserManager<UserEntity> userManager, JwtSetting jwtSetting)
    {
        _signInManager = signInManager;
        _jwtService = jwtService;
        _userManager = userManager;
        _jwtSetting = jwtSetting;
    }
    public async Task<ErrorOr<UserLoginResultDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return Error.Validation(description: "Invalid email or password");

            var claims = await _userManager.GetClaimsAsync(user);
            var subJwtClaim = new Claim(JwtRegisteredClaimNames.Sub, user.Id);
            claims.Add(subJwtClaim);

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