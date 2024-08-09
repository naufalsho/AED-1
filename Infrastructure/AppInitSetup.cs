using Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class AppInitSetup
    {
        public static void InitialSetup(this WebApplicationBuilder builder)
        {
            var appSett = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

            var basePath = Path.Combine(builder.Environment.WebRootPath + appSett.FileRepoPath);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
        }
    }
}
