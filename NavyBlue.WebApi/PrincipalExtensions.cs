using System.Security.Claims;
using System.Security.Principal;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     PrincipalExtensions.
    /// </summary>
    public static class PrincipalExtensions
    {
        /// <summary>
        ///     Determines whether the specified principal is application.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns><c>true</c> if the specified principal is application; otherwise, <c>false</c>.</returns>
        public static bool IsApplication(this IPrincipal principal)
        {
            return principal.IsRole("Application");
        }

        /// <summary>
        ///     Determines whether the specified principal is role.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>System.Boolean.</returns>
        public static bool IsRole(this IPrincipal principal, string roleName)
        {
            ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
            ClaimsIdentity claimsIdentity = claimsPrincipal?.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                return claimsIdentity.HasClaim(ClaimTypes.Role, roleName);
            }

            return false;
        }

        /// <summary>
        ///     Determines whether the specified principal is user.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>System.Boolean.</returns>
        public static bool IsUser(this IPrincipal principal)
        {
            return principal.IsRole("User");
        }
    }
}