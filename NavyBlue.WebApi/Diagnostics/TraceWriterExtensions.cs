using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Tracing;

namespace NavyBlue.AspNetCore.Web.Web.Diagnostics
{
    /// <summary>
    ///     Represents an extension methods for <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.
    /// </summary>
    public static class TraceWriterExtensions
    {

        /// <summary>
        ///     Displays an error message in the list with the specified writer, request, message format and argument.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="messageFormat">The format of the message.</param>
        /// <param name="messageArguments">The argument in the message.</param>
        public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, string messageFormat, params object[] messageArguments)
        {
            traceWriter.Error(request, "Application", TraceLevel.Error, messageFormat, messageArguments);
        }

        /// <summary>
        ///     Displays an error message in the list with the specified writer, request, and exception.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="exception">The error occurred during execution.</param>
        public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, Exception exception)
        {
            traceWriter.Error(request, "Application", TraceLevel.Error, exception);
        }

        /// <summary>
        ///     Displays an error message in the list with the specified writer, request, exception, message format and argument.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="messageFormat">The format of the message.</param>
        /// <param name="messageArguments">The argument in the message.</param>
        public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, Exception exception, string messageFormat, params object[] messageArguments)
        {
            traceWriter.Error(request, "Application", TraceLevel.Error, exception, messageFormat, messageArguments);
        }

        /// <summary>
        ///     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="messageFormat">The format of the message.</param>
        /// <param name="messageArguments">The message argument.</param>
        public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, string messageFormat, params object[] messageArguments)
        {
            traceWriter.Info(request, "Application", TraceLevel.Info, messageFormat, messageArguments);
        }

        /// <summary>
        ///     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="exception">The error occurred during execution.</param>
        public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, Exception exception)
        {
            traceWriter.Info(request, "Application", TraceLevel.Info, exception);
        }

        /// <summary>
        ///     Displays the details in the <see cref="System.Web.Http.Tracing.ITraceWriterExtensions" />.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="exception">The error occurred during execution.</param>
        /// <param name="messageFormat">The format of the message.</param>
        /// <param name="messageArguments">The message argument.</param>
        public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, Exception exception, string messageFormat, params object[] messageArguments)
        {
            traceWriter.Info(request, "Application", TraceLevel.Info, exception, messageFormat, messageArguments);
        }

        /// <summary>
        ///     Indicates the warning level of execution.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="messageFormat">The format of the message.</param>
        /// <param name="messageArguments">The message argument.</param>
        public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, string messageFormat, params object[] messageArguments)
        {
            traceWriter.Warn(request, "Application", TraceLevel.Warning, messageFormat, messageArguments);
        }

        /// <summary>
        ///     Indicates the warning level of execution.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="exception">The error occurred during execution.</param>
        public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, Exception exception)
        {
            traceWriter.Warn(request, "Application", TraceLevel.Warning, exception);
        }

        /// <summary>
        ///     Indicates the warning level of execution.
        /// </summary>
        /// <param name="traceWriter">The <see cref="T:System.Web.Http.Tracing.ITraceWriter" />.</param>
        /// <param name="request">The <see cref="T:System.Net.Http.HttpRequestMessage" /> with which to associate the trace. It may be null.</param>
        /// <param name="exception">The error occurred during execution.</param>
        /// <param name="messageFormat">The format of the message.</param>
        /// <param name="messageArguments">The message argument.</param>
        public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, Exception exception, string messageFormat, params object[] messageArguments)
        {
            traceWriter.Warn(request, "Application", TraceLevel.Warning, exception, messageFormat, messageArguments);
        }
    }
}