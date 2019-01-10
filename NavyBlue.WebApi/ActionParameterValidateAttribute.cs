using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Moe.Lib.Web
{
    /// <summary>
    ///     An action filter for validating action parameter, if validate failed, create a 400 response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionParameterValidateAttribute : OrderedActionFilterAttribute
    {
        /// <summary>
        ///     Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}