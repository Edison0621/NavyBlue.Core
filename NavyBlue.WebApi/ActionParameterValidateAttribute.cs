using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     An action filter for validating action parameter, if validate failed, create a 400 response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionParameterValidateAttribute : OrderedActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.ModelState);
            }
        }
    }
}