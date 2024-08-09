using Core;
using Domain.ExtServices;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class AppExternalSetup
    {
        public static IServiceCollection AddAppExternals(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientName.CommonClient);

            services.AddScoped<IIoTService, IoTService>();

            return services;
        }
    }
}
