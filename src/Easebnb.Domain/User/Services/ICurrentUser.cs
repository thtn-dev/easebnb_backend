

using ErrorOr;

namespace Easebnb.Domain.User.Services
{
    public interface ICurrentUser
    {
        ErrorOr<string> UserId { get; }
    }
}
