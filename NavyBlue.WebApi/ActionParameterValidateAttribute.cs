// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ActionParameterValidateAttribute.cs
// Created          : 2019-01-09  20:20
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:02
// *****************************************************************************************************************
// <copyright file="ActionParameterValidateAttribute.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     An action filter for validating action parameter, if validate failed, create a 400 response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionParameterValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.WriteAsync(context.ModelState.Values.ToString()).ConfigureAwait(false).GetAwaiter().GetResult();
                    //= context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, context.ModelState);
            }
        }
    }
}