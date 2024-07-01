using Easebnb.Application.Common.Interfaces;
using Easebnb.Domain.Common.Options;
using Easebnb.Domain.User;
using Easebnb.Infrastructure.Data.Contexts;
using Easebnb.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;

namespace Easebnb.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFramework(configuration);
        services.AddAspNetIdentity(configuration);
        return services;
    }
    /// <summary>
    /// Configure Entity Framework
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    private static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Entity Framework
        var dbOptions = configuration.GetSection(DatabaseSetting.SettingKey).Get<DatabaseSetting>();
        ArgumentNullException.ThrowIfNull(dbOptions, nameof(DatabaseSetting));
        services.AddSingleton(dbOptions);
        services.AddScoped<ISaveChangesInterceptor, DateTrackingInterceptor>();

        services.AddDbContext<IApplicationDbContext, AppDbContext>((sp, options) =>
        {
            var connBuilder = new NpgsqlConnectionStringBuilder()
            {
                Host = dbOptions.Host,
                Port = dbOptions.Port,
                Username = dbOptions.Username,
                Password = dbOptions.Password,
                Database = dbOptions.Database,
                SslMode = dbOptions.SslMode == "require" ? SslMode.Require : SslMode.Disable,
            };
            var conn = connBuilder.ToString();
            options.UseNpgsql(conn, builder =>
            {
                builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

    }

    /// <summary>
    /// Configure AspNet Identity
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    private static void AddAspNetIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<UserEntity, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        var jwtSetting = configuration.GetSection(JwtSetting.SettingKey).Get<JwtSetting>();
        ArgumentNullException.ThrowIfNull(jwtSetting, nameof(JwtSetting));
        services.AddSingleton(jwtSetting);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret)),
                };
            });
    }
}