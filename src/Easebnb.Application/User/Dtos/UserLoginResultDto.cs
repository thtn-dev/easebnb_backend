﻿
namespace Easebnb.Application.User.Dtos
{
    public class UserLoginResultDto
    {
        public required string Token { get; set; }
        public long ExpiresIn { get; set; }
        public required string Sub { get; set; }
    }
}
