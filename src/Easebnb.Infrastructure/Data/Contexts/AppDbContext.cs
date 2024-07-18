using Easebnb.Application.Common.Interfaces;
using Easebnb.Domain.Homestay;
using Easebnb.Domain.User;
using Easebnb.Infrastructure.Data.Builders;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Easebnb.Infrastructure.Data.Contexts;

public class AppDbContext : DbContext, IApplicationDbContext
{
    #region VN_AdministrativeUnit
    public DbConnection Connection => Database.GetDbConnection();

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<HomestayEntity> Homestays { get; set; }
    #endregion

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyIdentityModelBuilder();
        builder.ApplyHomestayModelBuilder();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}