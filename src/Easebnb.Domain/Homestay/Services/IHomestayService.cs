

namespace Easebnb.Domain.Homestay.Services
{
    public interface IHomestayService
    {
        Task<int> CreateHomstayAsync(HomestayEntity homestay);
    }
}
