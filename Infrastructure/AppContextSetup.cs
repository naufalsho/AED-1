using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class AppContextSetup
    {
    public static IServiceCollection AddAppDbContexts(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

            return services;
        }
    }
}
