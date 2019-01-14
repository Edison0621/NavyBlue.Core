// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ExceptionLogger.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:24
// *****************************************************************************************************************
// <copyright file="ExceptionLogger.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;
using System.Diagnostics;
using System.Text;

namespace NavyBlue.AspNetCore.Web.Handlers
{
    /// <summary>
    ///     ExceptionLogger.
    /// </summary>
    public class ExceptionLogger : System.Web.Http.ExceptionHandling.ExceptionLogger
    {
        /// <summary>
        ///     When overridden in a derived class, logs the exception synchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                // Retrieve the current HttpContext instance for this request.
                HttpContext httpContext = HttpUtils.GetHttpContext(context.Request);

                if (httpContext == null)
                {
                    return;
                }

                string request = httpContext.Request.Dump();

                StringBuilder messageBuilder = new StringBuilder();

                messageBuilder.AppendFormat("{0} {1} {2} {3}", httpContext.Request.HttpMethod, httpContext.Request.RawUrl, httpContext.Response.StatusCode, httpContext.Response.Status);
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(request);
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(context.Exception.GetExceptionString());

                Trace.TraceError(messageBuilder.ToString());
            }
            catch (Exception e)
            {
                Trace.TraceError("ExceptionLoggerInternalError" + e.Message);
            }
        }
    }
}