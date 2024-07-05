using Easebnb.Application.Common.Interfaces;
using Easebnb.Domain.User;
using Easebnb.Domain.VN_AdministrativeUnit;
using Easebnb.Infrastructure.Data.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Easebnb.Infrastructure.Data.Contexts;

public class AppDbContext : IdentityDbContext<UserEntity>, IApplicationDbContext, IVNAdministrativeUnitDbContext, IApplicationConnection
{
    #region VN_AdministrativeUnit
    public DbSet<AdministrativeUnitEntity> AdministrativeUnits { get; set; }
    public DbSet<AdministrativeRegionEntity> AdministrativeRegions { get; set; }
    public DbSet<ProvinceEntity> Provinces { get; set; }
    public DbSet<DistrictEntity> Districts { get; set; }
    public DbSet<WardEntity> Wards { get; set; }

    public DbConnection Connection => Database.GetDbConnection();
    #endregion

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyIdentityModelBuilder();
        // remove asp.net identity conventions
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName($"{tableName[6..]}");
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}