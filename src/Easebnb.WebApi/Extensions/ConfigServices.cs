using Easebnb.Application;
using Easebnb.Domain.User.Services;
using Easebnb.Shared;

namespace Easebnb.WebApi.Extensions
{
    public static class ConfigServices
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer(builder.Configuration);
            builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
            builder.Services.AddScoped<ICurrentUser, CurrentUser>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.ConfigureCors();

            return builder;
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultDevCorsPolicy", builder =>
                {
                    // allow all
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                options.AddPolicy("DefaultProdCorsPolicy", builder =>
                {
                    // allow only the specified origin
                    builder.WithOrigins("https://example.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}
