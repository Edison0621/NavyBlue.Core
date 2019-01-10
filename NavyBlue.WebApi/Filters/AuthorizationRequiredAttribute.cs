using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using NavyBlue.AspNetCore.Web.Web.Auth;
using Moe.Lib.Web;

namespace NavyBlue.AspNetCore.Web.Web.Filters
{
    /// <summary>
    ///     Class AuthorizationRequiredAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationRequiredAttribute : OrderedAuthorizationFilterAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorizationRequiredAttribute" /> class.
        /// </summary>
        public AuthorizationRequiredAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Web.Http.Filters.ActionFilterAttribute" /> class.
        /// </summary>
        public AuthorizationRequiredAttribute(string schemeName)
        {
            this.SchemeName = schemeName;
        }

        /// <summary>
        ///     Gets a value indicating whether [require authenticated].
        /// </summary>
        /// <value><c>true</c> if [require authenticated]; otherwise, <c>false</c>.</value>
        public bool RequireAuthenticated { get; set; } = true;

        /// <summary>
        ///     Gets or sets the name of the scheme.
        /// </summary>
        /// <value>The name of the scheme.</value>
        public string SchemeName { get; }

        public override void OnAuthorization(AuthorizationFilterContext context)
        {
            string schemeName = this.SchemeName ?? NBAuthScheme.Bearer;
            if (context.HttpContext.Request.Headers[HeaderNames.Scheme] == StringValues.Empty 
                || !string.Equals(context.HttpContext.Request.Headers[HeaderNames.Scheme], schemeName, StringComparison.OrdinalIgnoreCase))
            {
                //context.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
                //context.Response.Headers.Add("WWW-Authenticate", $"{this.SchemeName} relam=jinyinmao.com.cn");
                //return;
            }

            base.OnAuthorization(context);
        }
    }
}