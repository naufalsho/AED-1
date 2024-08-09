using Core.Models;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExt
    {
        public static int GetId(this ClaimsPrincipal principal)
        {
            var userSid = principal.FindFirst(ClaimTypes.Sid);

            return int.Parse(userSid.Value);
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var name = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (name == null)
                return "";

            return name.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(ClaimTypes.Name);

            if (userId == null)
                return "";

            return userId.Value;
        }

        public static int GetRoleId(this ClaimsPrincipal principal)
        {
            var userSid = principal.FindFirst(ClaimTypes.Role);

            return int.Parse(userSid.Value);
        }

        public static string GetRoleName(this ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst("RoleName");

            if (userId == null)
                return "";

            return userId.Value;
        }

        public static UserClaimModel GetUserClaims(this ClaimsPrincipal principal)
        {
            try
            {
                var ret = new UserClaimModel()
                {
                    Sid = int.Parse(principal.FindFirst(ClaimTypes.Sid).Value),
                    NameIdentifier = principal.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Name = principal.FindFirst(ClaimTypes.Name).Value,
                    RoleId = int.Parse(principal.FindFirst(ClaimTypes.Role).Value),
                    RoleName = principal.FindFirst("RoleName").Value
                };

                return ret;

            }
            catch
            {
                return null;
            }
        }
    }
}
