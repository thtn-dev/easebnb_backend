using Easebnb.Domain.User.Services;

namespace Easebnb.Infrastructure.User;

internal class UserNormalize : IUserNormalize
{
    public string NormalizeEmail(string email)
       => IUserNormalize.DefaultNormalize(email);

    public string NormalizeUserName(string userName)
        => IUserNormalize.DefaultNormalize(userName);
}