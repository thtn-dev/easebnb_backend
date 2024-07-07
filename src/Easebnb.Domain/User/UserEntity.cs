using Easebnb.Shared.Entities;

namespace Easebnb.Domain.User;

public class UserEntity : EntityBase<string>, IDateTracking, IAggregateRoot
{
    public string UserName { get; set; } = null!;
    public string NormalizedUserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string NormalizedEmail { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public bool EmailConfirmed { get; set; } 
    public string ConcurrencyStamp { get; set; } = null!;
    public string SecurityStamp { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static UserEntity Create(string userName, string email)
    {
        return new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName,
            Email = email,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
        };
    }


}