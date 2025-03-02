using System.Security.Claims;

namespace ClinicManager.API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static bool IsAdmin(this ClaimsPrincipal userClaim)
        {
            var role = userClaim.FindFirst(ClaimTypes.Role)?.Value;
            if (role == null || role != "Admin") { return false; }
            return true;
        }

        public static string UserName(this ClaimsPrincipal userClaim)
        {
            var userName = userClaim.FindFirst("userName")?.Value;
            if (userName != null) { return userName; }
            return "";
        }
    }
}
