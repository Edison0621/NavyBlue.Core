// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NBApiController.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:55
// *****************************************************************************************************************
// <copyright file="NBApiController.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     NBApiController.
    /// </summary>
    public abstract class NBApiController : BaseApiController
    {
        ///// <summary>
        /////     Provides a set of methods and properties that help debug your code with the specified writer, request, exception, message format and argument.
        ///// </summary>
        ///// <param name="message">The log message.</param>
        //protected void Debug(string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Debug, message);
        //}

        ///// <summary>
        /////     Provides a set of methods and properties that help debug your code with the specified writer, request, and exception.
        ///// </summary>
        ///// <param name="exception">The error occurred during execution.</param>
        //protected void Debug(Exception exception)
        //{
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Debug, exception);
        //}

        ///// <summary>
        /////     Provides a set of methods and properties that help debug your code with the specified writer, request, exception, message format and argument.
        ///// </summary>
        ///// <param name="exception">The error occurred during execution.</param>
        ///// <param name="message">The log message.</param>
        //protected void Debug(Exception exception, string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Debug, exception, message);
        //}

        ///// <summary>
        /////     Displays an error message in the list with the specified writer, request, message format and argument.
        ///// </summary>
        ///// <param name="message">The log message.</param>
        //protected void Error(string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Error, message);
        //}

        ///// <summary>
        /////     Displays an error message in the list with the specified writer, request, and exception.
        ///// </summary>
        ///// <param name="exception">The error occurred during execution.</param>
        //protected void Error(Exception exception)
        //{
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Error, exception);
        //}

        ///// <summary>
        /////     Displays an error message in the list with the specified writer, request, exception, message format and argument.
        ///// </summary>
        ///// <param name="exception">The exception.</param>
        ///// <param name="message">The log message.</param>
        //protected void Error(Exception exception, string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Error, exception, message);
        //}

        ///// <summary>
        /////     Displays an error message in the <see cref="T:System.Web.Http.Tracing.TraceWriterExtensions" /> class with the specified writer, request, and message format and argument.
        ///// </summary>
        ///// <param name="message">The log message.</param>
        //protected void Fatal(string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Fatal, message);
        //}

        ///// <summary>
        /////     Displays an error message in the <see cref="T:System.Web.Http.Tracing.TraceWriterExtensions" /> class with the specified writer, request, and exception.
        ///// </summary>
        ///// <param name="exception">The exception that appears during execution.</param>
        //protected void Fatal(Exception exception)
        //{
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Fatal, exception);
        //}

        ///// <summary>
        /////     Displays an error message in the <see cref="T:System.Web.Http.Tracing.TraceWriterExtensions" /> class with the specified writer, request, and exception, message format and argument.
        ///// </summary>
        ///// <param name="exception">The exception.</param>
        ///// <param name="message">The log message.</param>
        //protected void Fatal(Exception exception, string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Fatal, exception, message);
        //}

        ///// <summary>
        /////     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        ///// </summary>
        ///// <param name="message">The log message.</param>
        //protected void Info(string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Info, message);
        //}

        ///// <summary>
        /////     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        ///// </summary>
        ///// <param name="exception">The error occurred during execution.</param>
        //protected void Info(Exception exception)
        //{
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Info, exception);
        //}

        ///// <summary>
        /////     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        ///// </summary>
        ///// <param name="exception">The error occurred during execution.</param>
        ///// <param name="message">The log message.</param>
        //protected void Info(Exception exception, string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Info, exception, message);
        //}

        ///// <summary>
        /////     Indicates the warning level of execution.
        ///// </summary>
        ///// <param name="message">The log message.</param>
        //protected void Warn(string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Warn, message);
        //}

        ///// <summary>
        /////     Indicates the warning level of execution.
        ///// </summary>
        ///// <param name="exception">The error occurred during execution.</param>
        //protected void Warn(Exception exception)
        //{
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Warn, exception);
        //}

        ///// <summary>
        /////     Indicates the warning level of execution.
        ///// </summary>
        ///// <param name="exception">The error occurred during execution.</param>
        ///// <param name="message">The log message.</param>
        //protected void Warn(Exception exception, string message)
        //{
        //    message = message.Replace("{", "{{").Replace("}", "}}");
        //    this.Logger.Trace(this.Request, "Application", TraceLevel.Warn, exception, message);
        //}
    }
}