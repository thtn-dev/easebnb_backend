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
        builder.Entity<UserEntity>(ConfigureUser);
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
            .HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);

        entity
            .Property(x => x.LoginProvider)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.Name)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);
    }

    private static void ConfigureUserLogin(EntityTypeBuilder<IdentityUserLogin<string>> entity)
    {
        entity.ToTable("UserLogins")
            .HasKey(x => new { x.LoginProvider, x.ProviderKey });

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);

        entity
            .Property(x => x.LoginProvider)
            .IsRequired()
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.ProviderDisplayName)
            .IsRequired()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);
    }

    private static void ConfigureUserClaim(EntityTypeBuilder<IdentityUserClaim<string>> entity)
    {
        entity.ToTable("UserClaims")
            .HasKey(x => x.Id);

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);

        entity
            .Property(x => x.ClaimType)
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.ClaimValue)
            .HasColumnType(DbTypeConstants.VARCHAR512)
            .HasMaxLength(512);
    }

    private static void ConfigureUserRole(EntityTypeBuilder<IdentityUserRole<string>> entity)
    {
        entity.ToTable("UserRoles")
            .HasKey(x => new { x.UserId, x.RoleId });

        entity
            .Property(x => x.UserId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);
        entity
            .Property(x => x.RoleId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);
    }

    private static void ConfigureRole(EntityTypeBuilder<IdentityRole> entity)
    {
        entity.ToTable("Roles")
            .HasKey(x => x.Id);

        entity
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);

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
            .Property(x => x.RoleId)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);

        entity
            .Property(x => x.ClaimType)
            .IsUnicode()
            .HasColumnType(DbTypeConstants.VARCHAR256)
            .HasMaxLength(256);

        entity
            .Property(x => x.ClaimValue)
            .HasColumnType(DbTypeConstants.VARCHAR512)
            .HasMaxLength(512);
    }

    private static void ConfigureUser(EntityTypeBuilder<UserEntity> entity)
    {
        entity.ToTable("Users")
            .HasKey(x => x.Id);

        entity
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnType(DbTypeConstants.Guid);

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
            .HasColumnType(DbTypeConstants.Email)
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
            .HasColumnType(DbTypeConstants.VARCHAR128)
            .HasMaxLength(128);

        entity
            .Property(x => x.PhoneNumber)
            .IsUnicode()
            .HasColumnType(DbTypeConstants.PhoneNumber)
            .HasMaxLength(20);

        entity
            .Property(x => x.CreatedAt)
            .IsRequired();

        entity
            .Property(x => x.UpdatedAt)
            .IsRequired(false)
            .HasDefaultValue(null);

    }
}