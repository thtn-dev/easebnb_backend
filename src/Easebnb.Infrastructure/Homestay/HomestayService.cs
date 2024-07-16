using Easebnb.Application.Common.Interfaces;
using Easebnb.Domain.Homestay;
using Easebnb.Domain.Homestay.Services;
using Microsoft.EntityFrameworkCore;

namespace Easebnb.Infrastructure.Homestay
{
    public class HomestayService : IHomestayService
    {
        private readonly IApplicationDbContext _appContext;
        public HomestayService(IApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        public async Task<int> CreateHomstayAsync(HomestayEntity homestay, CancellationToken cancellation = default)
        {
            await _appContext.Homestays.AddAsync(homestay, cancellation).ConfigureAwait(false);
            return await _appContext.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<HomestayEntity?> GetHomestayByIdAsync(long id , CancellationToken cancellation = default)
        {
            var result = await _appContext.Homestays.
                Where(h => h.Id == id).Select(h => new
                {
                    h.Id,
                    h.Name,
                    h.Description,
                    h.Geom
                }).Select(h => new HomestayEntity
                {
                    Id = h.Id,
                    Name = h.Name,
                    Description = h.Description,
                    Geom = h.Geom
                }).FirstOrDefaultAsync(cancellation).ConfigureAwait(false);
            return result;
        }
    }
}
