// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NBExceptionLogger.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:23
// *****************************************************************************************************************
// <copyright file="NBExceptionLogger.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.AspNetCore.Web.Handlers;
using NavyBlue.NetCore.Lib;
using NavyBlue.NetCore.Lib.Loggers;
using System;

namespace NavyBlue.AspNetCore.Web.Diagnostics
{
    /// <summary>
    ///     NBExceptionLogger.
    /// </summary>
    public sealed class NBExceptionLogger : ExceptionLogger
    {
        private static readonly Lazy<IWebLogger> logger = new Lazy<IWebLogger>(() => InitApplicationLogger());

        private IWebLogger Logger
        {
            get { return logger.Value; }
        }

        /// <summary>
        ///     When overridden in a derived class, logs the exception synchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        public override void Log(ExceptionLoggerContext context)
        {
            TraceEntry traceEntry = context.Request?.GetTraceEntry();

            this.Logger.Log(2, context.Exception.Message, context.Request, "ASP.NET Error", 0UL, string.Empty, traceEntry, context.Exception);
        }

        private static IWebLogger InitApplicationLogger()
        {
            return App.LogManager.CreateWebLogger();
        }
    }
}