using Easebnb.Application;
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
            return builder;
        }
    }
}
