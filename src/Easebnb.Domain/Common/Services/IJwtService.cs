﻿using System.Security.Claims;

namespace Easebnb.Domain.Common.Services;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(IEnumerable<Claim> claims);
}