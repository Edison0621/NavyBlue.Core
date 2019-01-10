// ***********************************************************************
// Project          : MoeLib
// File             : JinyinmaoApiController.cs
// Created          : 2015-11-23  3:57 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-27  12:41 AM
// ***********************************************************************
// <copyright file="JinyinmaoApiController.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Web.Http.Tracing;
using MoeLib.Web;

namespace NavyBlue.AspNetCore.Web.Web
{
    /// <summary>
    ///     JinyinmaoApiController.
    /// </summary>
    public abstract class JinyinmaoApiController : MoeApiController
    {
        /// <summary>
        ///     Displays an error message in the list with the specified writer, request, message format and argument.
        /// </summary>
        /// <param name="message">The log message.</param>
        protected void Error(string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Error, message);
        }

        /// <summary>
        ///     Displays an error message in the list with the specified writer, request, and exception.
        /// </summary>
        /// <param name="exception">The error occurred during execution.</param>
        protected void Error(Exception exception)
        {
            this.Logger.Trace(this.Request, "Application", TraceLevel.Error, exception);
        }

        /// <summary>
        ///     Displays an error message in the list with the specified writer, request, exception, message format and argument.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The log message.</param>
        protected void Error(Exception exception, string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Error, exception, message);
        }

        /// <summary>
        ///     Displays an error message in the <see cref="T:System.Web.Http.Tracing.TraceWriterExtensions" /> class with the specified writer, request, and message format and argument.
        /// </summary>
        /// <param name="message">The log message.</param>
        protected void Fatal(string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Fatal, message);
        }

        /// <summary>
        ///     Displays an error message in the <see cref="T:System.Web.Http.Tracing.TraceWriterExtensions" /> class with the specified writer, request, and exception.
        /// </summary>
        /// <param name="exception">The exception that appears during execution.</param>
        protected void Fatal(Exception exception)
        {
            this.Logger.Trace(this.Request, "Application", TraceLevel.Fatal, exception);
        }

        /// <summary>
        ///     Displays an error message in the <see cref="T:System.Web.Http.Tracing.TraceWriterExtensions" /> class with the specified writer, request, and exception, message format and argument.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The log message.</param>
        protected void Fatal(Exception exception, string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Fatal, exception, message);
        }

        /// <summary>
        ///     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        /// </summary>
        /// <param name="message">The log message.</param>
        protected void Info(string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Info, message);
        }

        /// <summary>
        ///     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        /// </summary>
        /// <param name="exception">The error occurred during execution.</param>
        protected void Info(Exception exception)
        {
            this.Logger.Trace(this.Request, "Application", TraceLevel.Info, exception);
        }

        /// <summary>
        ///     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        /// </summary>
        /// <param name="exception">The error occurred during execution.</param>
        /// <param name="message">The log message.</param>
        protected void Info(Exception exception, string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Info, exception, message);
        }

        /// <summary>
        ///     Indicates the warning level of execution.
        /// </summary>
        /// <param name="message">The log message.</param>
        protected void Warn(string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Warning, message);
        }

        /// <summary>
        ///     Indicates the warning level of execution.
        /// </summary>
        /// <param name="exception">The error occurred during execution.</param>
        protected void Warn(Exception exception)
        {
            this.Logger.Trace(this.Request, "Application", TraceLevel.Warning, exception);
        }

        /// <summary>
        ///     Indicates the warning level of execution.
        /// </summary>
        /// <param name="exception">The error occurred during execution.</param>
        /// <param name="message">The log message.</param>
        protected void Warn(Exception exception, string message)
        {
            message = message.Replace("{", "{{").Replace("}", "}}");
            this.Logger.Trace(this.Request, "Application", TraceLevel.Warning, exception, message);
        }
    }
}