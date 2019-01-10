// ***********************************************************************
// Project          : MoeLib
// File             : WADWebLogger.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-26  4:10 PM
// ***********************************************************************
// <copyright file="WADWebLogger.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NavyBlue.Lib;
using MoeLib.Diagnostics;
using NavyBlue.AspNetCore.Web.Diagnostics;

namespace NavyBlue.AspNetCore.Web.Web.Diagnostics
{
    /// <summary>
    ///     WADWebLogger.
    /// </summary>
    public class WADWebLogger : WADLogger, IWebLogger
    {
        #region IWebLogger Members

        /// <summary>
        ///     Criticals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="request">The request.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Critical(string message, HttpRequestMessage request, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            MessageContent logMessageContent = BuildLogMessageContent(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            Trace.TraceError(logMessageContent.ToJson());
        }

        /// <summary>
        ///     Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="request">The request.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Error(string message, HttpRequestMessage request, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            MessageContent logMessageContent = BuildLogMessageContent(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            Trace.TraceError(logMessageContent.ToJson());
        }

        /// <summary>
        ///     Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="request">The request.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Info(string message, HttpRequestMessage request, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            MessageContent logMessageContent = BuildLogMessageContent(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            Trace.TraceInformation(logMessageContent.ToJson());
        }

        /// <summary>
        ///     Logs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="request">The request.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Log(int level, string message, HttpRequestMessage request, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            switch (level)
            {
                case 1:
                    this.Critical(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 2:
                    this.Error(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 3:
                    this.Warning(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 4:
                    this.Info(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 5:
                    this.Verbose(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                default:
                    this.Verbose(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;
            }
        }

        /// <summary>
        ///     Verboses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="request">The request.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Verbose(string message, HttpRequestMessage request, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            // Do nothing.
        }

        /// <summary>
        ///     Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="request">The request.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Warning(string message, HttpRequestMessage request, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            MessageContent logMessageContent = BuildLogMessageContent(message, request, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            Trace.TraceWarning(logMessageContent.ToJson());
        }

        #endregion IWebLogger Members

        private static MessageContent BuildLogMessageContent(string message, HttpRequestMessage request, string tag, ulong errorCode, string errorCodeMessage, TraceEntry traceEntry, Exception exception, Dictionary<string, object> payload)
        {
            if (request != null)
            {
                if (payload == null)
                {
                    payload = new Dictionary<string, object>();
                }

                payload.AddIfNotExist("HttpMethod", request.Method?.Method);

                payload.AddIfNotExist("RequestUri", request.RequestUri?.OriginalString);

                if (request.Headers != null)
                {
                    payload.AddIfNotExist("Referrer", request.Headers.Referrer?.ToString());
                    payload.AddIfNotExist("UserAgent", request.Headers.UserAgent?.ToString());

                    request.Headers.Where(h => h.Key.StartsWith("X-", StringComparison.OrdinalIgnoreCase)).ToList()
                        .ForEach(h => payload.Add(h.Key, h.Value.Join(",")));
                }

                if (request.Content != null)
                {
                    Task<string> contentTask = request.Content.ReadAsStringAsync();
                    contentTask.Wait();
                    if (!contentTask.Result.Contains("password", StringComparison.OrdinalIgnoreCase))
                    {
                        payload.AddIfNotExist("RequestContent", contentTask.Result);
                    }
                }
            }

            return BuildLogMessageContent(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
        }
    }
}