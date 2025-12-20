using BuildingBlocks.Exceptions;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API
{
    public static class DependencyInjecton
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration) // Add IConfiguration parameter
        {
            // Add API services here, e.g., Controllers, Swagger, CORS, etc.
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database")!); // Now configuration is in scope
            return services;
        }
        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure the HTTP request pipeline here, e.g., Swagger, CORS, etc.
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
                });
            return app;
        }
    }
}
