using Hangfire.Dashboard;

namespace Infrastructure.Filters
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        private readonly string _allowedRole;

        public HangfireAuthFilter(string allowedRole)
        {
            _allowedRole = allowedRole;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var identities = httpContext.User.Identities;

            if (identities.Any())
            {
                var claims = identities.First().Claims.ToList();

                if (claims.Any())
                {
                    return claims.First(m => m.Type == "RoleName")?.Value == _allowedRole;
                }
            }

            return false;
        }
    }
}
