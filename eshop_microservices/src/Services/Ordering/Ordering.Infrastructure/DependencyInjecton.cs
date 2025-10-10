using Microsoft.EntityFrameworkCore;

namespace Ordering.Infrastructure
{
    public static class DependencyInjecton
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddDbContext<ApplicationDbContext>(options =>

                options.UseSqlServer(connectionString));
             
            

            return services;    
        }
    }
}
