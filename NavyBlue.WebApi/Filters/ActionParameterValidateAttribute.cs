// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ActionParameterValidateAttribute.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:23
// *****************************************************************************************************************
// <copyright file="ActionParameterValidateAttribute.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace NavyBlue.AspNetCore.Web.Filters
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
                StringBuilder errors = new StringBuilder();

                foreach (KeyValuePair<string, ModelState> keyValuePair in actionContext.ModelState)
                {
                    foreach (ModelError modelError in keyValuePair.Value.Errors)
                    {
                        errors.Append(modelError.ErrorMessage);
                    }
                }

                actionContext.Response = new HttpResponseMessage
                {
                    Content = new StringContent(new { Message = errors.ToString() }.ToJson()),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}