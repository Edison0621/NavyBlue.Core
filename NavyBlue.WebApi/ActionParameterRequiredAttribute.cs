using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Moe.Lib.Web
{
    /// <summary>
    ///     An action filter for checking whether the action parameter is null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionParameterRequiredAttribute : OrderedActionFilterAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ActionParameterRequiredAttribute" /> class.
        /// </summary>
        /// <param name="actionParameterName">Name of the action parameter.</param>
        /// <exception cref="System.ArgumentNullException">actionParameterName</exception>
        public ActionParameterRequiredAttribute(string actionParameterName = "request")
        {
            this.ActionParameterName = actionParameterName;
        }

        /// <summary>
        ///     Gets the name of the action parameter.
        /// </summary>
        /// <value>The name of the action parameter.</value>
        public string ActionParameterName { get; }

        /// <summary>
        ///     Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            object parameterValue;
            if (actionContext.ActionArguments.TryGetValue(this.ActionParameterName, out parameterValue))
            {
                if (parameterValue == null)
                {
                    string errorMessage = this.FormatErrorMessage();
                    actionContext.ModelState.AddModelError(this.ActionParameterName, errorMessage);
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
                }
            }
        }

        /// <summary>
        ///     Formats the error message.
        /// </summary>
        /// <returns>System.String.</returns>
        private string FormatErrorMessage()
        {
            return $"The action parameter {this.ActionParameterName} cannot be null.";
        }
    }
}