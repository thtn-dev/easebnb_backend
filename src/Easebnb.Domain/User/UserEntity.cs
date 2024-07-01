using Easebnb.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Easebnb.Domain.User;

public class UserEntity : IdentityUser, IEntityBase<string>, IDateTracking, IAggregateRoot
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserEntity()
    {
        // Required by EF
    }
}