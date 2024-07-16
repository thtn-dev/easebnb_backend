using Easebnb.Application.Common.Interfaces;
using Easebnb.Domain.Homestay;
using Easebnb.Domain.Homestay.Services;

namespace Easebnb.Infrastructure.Homestay
{
    public class HomestayService : IHomestayService
    {
        private readonly IApplicationDbContext _appContext;
        public HomestayService(IApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        public Task<int> CreateHomstayAsync(HomestayEntity homestay)
        {
            _appContext.Homestays.Add(homestay);
            return _appContext.SaveChangesAsync();
        }
    }
}
