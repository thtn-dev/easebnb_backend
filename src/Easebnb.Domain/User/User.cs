using Ardalis.GuardClauses;
using Easebnb.Domain.Common.ValueObjects;
using Easebnb.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Easebnb.Domain.User;

public class User : IdentityUser, IEntityBase<string>, IDateTracking, IAggregateRoot
{
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private User()
    {
        // Required by EF
    }
}