using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using NavyBlue.AspNetCore.Web;
using NavyBlue.Lib.Web;

namespace NavyBlue.AspNetCore.Web.Filters
{
    /// <summary>
    ///     IPAuthorizeAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IPAuthorizeAttribute : OrderedAuthorizationFilterAttribute
    {
        /// <summary>
        ///     Gets or sets the valiad ip regex.
        /// </summary>
        /// <value>The valiad ip regex.</value>
        public Regex ValiadIPRegex { get; set; }

        /// <summary>
        ///     Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using <see cref="T:System.Web.Http.Filters.AuthorizationFilterAttribute" />.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!this.IpIsAuthorized(actionContext))
            {
                this.HandleUnauthorizedRequest(actionContext);
                return;
            }

            base.OnAuthorization(actionContext);
        }

        /// <summary>
        ///     Processes requests that fail authorization. This default implementation creates a new response with the
        ///     Unauthorized status code. Override this method to provide your own handling for unauthorized requests.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        /// <exception cref="System.ArgumentNullException">@actionContext can not be null</exception>
        private void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext), @"actionContext can not be null");
            }

            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "");
        }

        /// <summary>
        ///     Ips the is authorized.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool IpIsAuthorized(HttpActionContext context)
        {
            HttpRequestMessage request = context.Request;
            string ip = HttpUtils.GetUserHostAddress(request);

            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            if (this.ValiadIPRegex == null)
            {
                return request.IsLocal() || ip == "::1";
            }

            return this.ValiadIPRegex.IsMatch(ip) || request.IsLocal() || ip == "::1";
        }
    }
}