// *****************************************************************************************************************
// Project          : NavyBlue
// File             : JinyinmaoTraceWriter.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  14:59
// *****************************************************************************************************************
// <copyright file="JinyinmaoTraceWriter.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using MoeLib.Diagnostics;
using NavyBlue.WebApi;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace NavyBlue.AspNetCore.Web.Diagnostics
{
    /// <summary>
    ///     Asp.Net TraceWriter for Jinyinmao.
    /// </summary>
    public sealed class JinyinmaoTraceWriter : ITraceWriter
    {
        private static readonly Lazy<IWebLogger> logger = new Lazy<IWebLogger>(() => InitApplicationLogger());

        private IWebLogger Logger
        {
            get { return logger.Value; }
        }

        #region ITraceWriter Members

        public TraceLevel LevelFilter => throw new NotImplementedException();

        public void Trace(TraceLevel level, string message, Exception ex)
        {
            throw new NotImplementedException();
        }

        #endregion ITraceWriter Members

        /// <summary>
        ///     The loggers
        /// </summary>
        /// <summary>
        ///     Gets the current logger.
        /// </summary>
        /// <value>The current logger.</value>
        /// <summary>
        ///     Invokes the specified traceAction to allow setting values in a new <see cref="T:System.Web.Http.Tracing.TraceRecord" /> if and only if tracing is permitted at the given category and level.
        /// </summary>
        /// <param name="request">The current <see cref="T:System.Net.Http.HttpRequestMessage" />.   It may be null but doing so will prevent subsequent trace analysis  from correlating the trace to a particular request.</param>
        /// <param name="category">The logical category for the trace.  Users can define their own.</param>
        /// <param name="level">The <see cref="T:System.Web.Http.Tracing.TraceLevel" /> at which to write this trace.</param>
        /// <param name="traceAction">The action to invoke if tracing is enabled.  The caller is expected to fill in the fields of the given <see cref="T:System.Web.Http.Tracing.TraceRecord" /> in this action.</param>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level == TraceLevel.Off || category != "Application") return;

            TraceRecord record = new TraceRecord(request, category, level);

            traceAction(record);
            this.LogTraceRecord(record);
        }

        private static int GetLogLevel(TraceLevel traceLevel)
        {
            return (int)traceLevel;
        }

        private static IWebLogger InitApplicationLogger()
        {
            return App.LogManager.CreateWebLogger();
        }

        /// <summary>
        ///     Logs the trace record.
        /// </summary>
        /// <param name="traceRecord">The trace record.</param>
        private void LogTraceRecord(TraceRecord traceRecord)
        {
            TraceEntry traceEntry = traceRecord.Request?.GetTraceEntry();

            this.Logger.Log(GetLogLevel(traceRecord.Level), traceRecord.Message, traceRecord.Request, "ASP.NET Trace", 0UL, string.Empty, traceEntry, traceRecord.Exception);
        }
    }
}