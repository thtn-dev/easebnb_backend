using Easebnb.Domain.Common.Constants;
using Easebnb.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Easebnb.Infrastructure.Data.Builders;
public static partial class IdentityBuilder
{
    public static void ApplyIdentityModelBuilder(this ModelBuilder builder)
    {
        builder.Entity<User>(ConfigureUser);
        builder.Entity<IdentityRole>(ConfigureRole);
        builder.Entity<IdentityUserRole<string>>(ConfigureUserRole);
        builder.Entity<IdentityUserClaim<string>>(ConfigureUserClaim);
        builder.Entity<IdentityRoleClaim<string>>(ConfigureRoleClaim);
        builder.Entity<IdentityUserLogin<string>>(ConfigureUserLogin);
        builder.Entity<IdentityUserToken<string>>(ConfigureUserToken);
    }
    private static void ConfigureUserToken(EntityTypeBuilder<IdentityUserToken<string>> entity)
    {
        entity.ToTable("UserTokens")
            .HasKey(x => new { x.UserId });

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR64);
    }

    private static void ConfigureUserLogin(EntityTypeBuilder<IdentityUserLogin<string>> entity)
    {
        entity.ToTable("UserLogins")
            .HasKey(x => x.UserId);

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR64);
    }

    private static void ConfigureUserClaim(EntityTypeBuilder<IdentityUserClaim<string>> entity)
    {
        entity.ToTable("UserClaims")
            .HasKey(x => x.Id);

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR32);
    }

    private static void ConfigureUserRole(EntityTypeBuilder<IdentityUserRole<string>> entity)
    {
        entity.ToTable("UserRoles")
            .HasKey(x => new { x.UserId, x.RoleId });

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR64);
        entity
            .Property(x => x.RoleId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR64);
    }

    private static void ConfigureRole(EntityTypeBuilder<IdentityRole> entity)
    {
        entity.ToTable("Roles")
            .HasKey(x => x.Id);

        entity
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnType("varchar(50)");

        // entity.HasIndex(x => x.Name).IsUnique();
        entity
            .Property(x => x.Name)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);
    }
    private static void ConfigureRoleClaim(EntityTypeBuilder<IdentityRoleClaim<string>> entity)
    {
        entity.ToTable("RoleClaims")
            .HasKey(x => x.Id);

        entity
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR64);
    }

    private static void ConfigureUser(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("Users")
            .HasKey(x => x.Id);

        entity
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR64);

        entity
            .Property(x => x.UserName)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.NormalizedUserName)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.Email)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.NormalizedEmail)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.PasswordHash)
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        
        entity
            .Property(x => x.PhoneNumber)
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR32)
            .HasMaxLength(32);

    }   
}