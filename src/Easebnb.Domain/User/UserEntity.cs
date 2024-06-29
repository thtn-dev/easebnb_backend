using Ardalis.GuardClauses;
using Easebnb.Domain.Common.ValueObjects;
using Easebnb.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Easebnb.Domain.User;

public class UserEntity : IdentityUser, IEntityBase<string>, IDateTracking, IAggregateRoot
{
    public new UserName? UserName { get; private set; }
    public new Email? Email { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserEntity(UserName userName, Email email)
    {
        Id = Guid.NewGuid().ToString();
        UserName = userName;
        Email = email;
        CreatedAt = DateTime.Now;
    }

    public void SetUserName(string userName)
    {
        Guard.Against.NullOrEmpty(userName, nameof(userName));
        UserName = UserName.Create(userName);
    }

    public void SetEmail(string email)
    {
        Guard.Against.NullOrEmpty(email, nameof(email));
        Email = Email.Create(email);
    }

    private UserEntity()
    {
        // Required by EF
    }
}