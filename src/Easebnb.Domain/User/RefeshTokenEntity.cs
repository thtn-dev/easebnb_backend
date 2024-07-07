﻿using Easebnb.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Easebnb.Domain.User;

public class RefeshTokenEntity : EntityBase<string>
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime Created { get; set; }
    public string RevokedByIp { get; set; }
    public DateTime? Revoked { get; set; }
    public string ReplacedByToken { get; set; }

    [NotMapped]
    public bool IsActive => Revoked == null && !IsExpired;
    [NotMapped]
    public bool IsExpired => DateTime.UtcNow >= Expires;

    public static string GenerateRFToken()
    {
        // generate a 64-byte array using a secure random number generator
        byte[] randomBytes = new byte[64];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}