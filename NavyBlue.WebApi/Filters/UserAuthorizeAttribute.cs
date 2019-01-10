using Microsoft.AspNetCore.Authorization;

namespace NavyBlue.AspNetCore.Web.Web.Filters
{
    /// <summary>
    ///     UserAuthorizeAttribute.
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAuthorizeAttribute" /> class.
        /// </summary>
        public UserAuthorizeAttribute()
        {
            this.Roles = "User";
        }
    }
}