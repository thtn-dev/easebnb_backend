using System.Security.Claims;

namespace Easebnb.Domain.User.Services;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(IEnumerable<Claim> claims);
}