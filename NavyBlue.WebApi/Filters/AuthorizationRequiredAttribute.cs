// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AuthorizationRequiredAttribute.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:23
// *****************************************************************************************************************
// <copyright file="AuthorizationRequiredAttribute.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using NavyBlue.NetCore.Lib;

namespace NavyBlue.AspNetCore.Web.Filters
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

        /// <summary>
        ///     Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using <see cref="T:System.Web.Http.Filters.AuthorizationFilterAttribute" />.</param>
        public override void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            string schemeName = this.SchemeName ?? NBAuthScheme.Bearer;
            if (actionContext.HttpContext.Request.Headers[HeaderNames.Authorization] == StringValues.Empty 
                || !string.Equals(actionContext.Request.Headers.Authorization.Scheme, schemeName, StringComparison.OrdinalIgnoreCase))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
                actionContext.Response.Headers.Add("WWW-Authenticate", $"{this.SchemeName} relam=nb.com.cn");
                return;
            }

            base.OnAuthorization(actionContext);
        }
    }
}