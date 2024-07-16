﻿

using Easebnb.Domain.Homestay.Dto;

namespace Easebnb.Domain.Homestay.Services
{
    public interface IHomestayService
    {
        Task<int> CreateHomstayAsync(HomestayEntity homestay, CancellationToken cancellation = default);
        Task<HomestayEntity?> GetHomestayByIdAsync(long id, CancellationToken cancellation = default);
        Task<List<HomestayEntity>> FindHomestayNearest(double longitude, double latitude, double tolerance, CancellationToken cancellation = default);
    }
}
