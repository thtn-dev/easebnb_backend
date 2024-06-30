
namespace Easebnb.Shared;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellation = default);
}
