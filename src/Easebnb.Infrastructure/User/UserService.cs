using Easebnb.Application.Common.Interfaces;
using Easebnb.Domain.User;
using Easebnb.Domain.User.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Easebnb.Infrastructure.User;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserNormalize _userNormalize;

    public UserService(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<IApplicationDbContext>() ?? throw new ArgumentNullException(nameof(IApplicationDbContext));
        _passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher>() ?? throw new ArgumentNullException(nameof(IPasswordHasher));
        _userNormalize = serviceProvider.GetRequiredService<IUserNormalize>() ?? throw new ArgumentNullException(nameof(IUserNormalize));
    }
    public IQueryable<UserEntity> Users => _context.Users.AsQueryable();

    public async Task<ErrorOr<UserEntity>> CreateUserAsync(UserEntity user, string password)
    {
        var hashedPassword = _passwordHasher.HashPassword(password);
        user.PasswordHash = hashedPassword;
        user.NormalizedEmail = _userNormalize.NormalizeEmail(user.Email);
        user.NormalizedUserName = _userNormalize.NormalizeUserName(user.UserName);
        await _context.Users.AddAsync(user);
        var rowAff = await _context.SaveChangesAsync();
        if (rowAff == 0)
        {
            return Error.Conflict("User already exists");
        }
        return user;
    }

    public Task DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        var emailNormalized = _userNormalize.NormalizeEmail(email);
        var user = await Users.FirstOrDefaultAsync(u => u.NormalizedEmail == emailNormalized);
        return user;
    }

    public Task<UserEntity?> GetUserByIdAsync(Guid userId)
    {
        return Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
    }

    public Task<UserEntity?> GetUserByUserNameAsync(string userName)
    {
        var userNameNormalized = _userNormalize.NormalizeUserName(userName);
        return Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userNameNormalized);
    }

    public async Task<ErrorOr<UserEntity>> SignInAsync(string userName, string password)
    {
        var user = await GetUserByUserNameAsync(userName);
        if (user == null)
        {
            return Error.NotFound("User not found");
        }
        var isVerified = _passwordHasher.VerifyPassword(password, user.PasswordHash);
        return isVerified ? user : Error.Validation("Invalid password");
    }

    public Task<UserEntity> UpdateUserAsync(Guid userId, string email, string password)
    {
        throw new NotImplementedException();
    }
}