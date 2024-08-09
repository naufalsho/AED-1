using Core.Models;
using Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class AppSettingSetup
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config.GetSection(nameof(AppSettings)));
            services.Configure<ExtConfigs>(config.GetSection(nameof(ExtConfigs)));

            services.AddScoped<AuthFilter>();

            return services;
        }

    }
}
