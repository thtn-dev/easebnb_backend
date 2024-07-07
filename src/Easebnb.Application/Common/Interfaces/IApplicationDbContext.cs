using Easebnb.Domain.User;
using Easebnb.Domain.VN_AdministrativeUnit;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Easebnb.Application.Common.Interfaces;
public interface IApplicationDbContext : IIdentityDbContext, IApplicationConnection
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IIdentityDbContext
{
    DbSet<UserEntity> Users { get; set; }
}

public interface IVNAdministrativeUnitDbContext
{
    DbSet<AdministrativeUnitEntity> AdministrativeUnits { get; set; }
    DbSet<AdministrativeRegionEntity> AdministrativeRegions { get; set; }
    DbSet<ProvinceEntity> Provinces { get; set; }
    DbSet<DistrictEntity> Districts { get; set; }
    DbSet<WardEntity> Wards { get; set; }
}

public interface IApplicationConnection
{
    DbConnection Connection { get; }
}