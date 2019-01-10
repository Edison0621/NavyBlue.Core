// ***********************************************************************
// Project          : MoeLib
// File             : NLogger.cs
// Created          : 2015-11-23  5:22 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  2:45 PM
// ***********************************************************************
// <copyright file="NLogger.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using MoeLib.Diagnostics;
using NavyBlue.Lib;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using ILogger = MoeLib.Diagnostics.ILogger;

namespace NavyBlue.AspNetCore.Web.Diagnostics
{
    /// <summary>
    ///     NLogger.
    /// </summary>
    public class NLogger : ILogger
    {
        /// <summary>
        ///     The application logger
        /// </summary>
        private static readonly Lazy<NLog.ILogger> ApplicationLogger = new Lazy<NLog.ILogger>(() => InitApplicationLogger());

        /// <summary>
        ///     Initializes static members of the <see cref="NLogger" /> class.
        /// </summary>
        static NLogger()
        {
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget target = new FileTarget
            {
                AutoFlush = true,
                CreateDirs = true,
                Encoding = Encoding.UTF8,
                FileName = Layout.FromString(AppDomain.CurrentDomain.BaseDirectory + "Logs\\${shortdate}.log"),
                Layout = Layout.FromString("${message}")
            };

            config.AddTarget("ApplicationTarget", target);

            config.LoggingRules.Add(new LoggingRule("ApplicationLogger", LogLevel.Info, target));

            NLog.LogManager.Configuration = config;
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected NLog.ILogger Logger
        {
            get { return ApplicationLogger.Value; }
        }

        #region ILogger Members

        /// <summary>
        ///     Criticals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Critical(string message, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            LogEntry logEntry = BuildLogEntry(1, message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            this.Logger.Fatal(logEntry.ToJson());
        }

        /// <summary>
        ///     Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Error(string message, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            LogEntry logEntry = BuildLogEntry(2, message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            this.Logger.Error(logEntry.ToJson());
        }

        /// <summary>
        ///     Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Info(string message, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            LogEntry logEntry = BuildLogEntry(4, message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            this.Logger.Info(logEntry.ToJson());
        }

        /// <summary>
        ///     Logs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Log(int level, string message, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            switch (level)
            {
                case 1:
                    this.Critical(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 2:
                    this.Error(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 3:
                    this.Warning(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 4:
                    this.Info(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                case 5:
                    this.Verbose(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;

                default:
                    this.Verbose(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
                    break;
            }
        }

        /// <summary>
        ///     Verboses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Verbose(string message, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            LogEntry logEntry = BuildLogEntry(5, message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            this.Logger.Debug(logEntry.ToJson());
        }

        /// <summary>
        ///     Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Warning(string message, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            LogEntry logEntry = BuildLogEntry(3, message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
            this.Logger.Warn(logEntry.ToJson());
        }

        #endregion ILogger Members

        /// <summary>
        ///     Criticals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Critical(string message, string clientId, string deviceId, string requestId, string sessionId, string userId, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", Exception exception = null, Dictionary<string, object> payload = null)
        {
            TraceEntry traceEntry = new TraceEntry
            {
                ClientId = clientId,
                DeviceId = deviceId,
                RequestId = requestId,
                SessionId = sessionId,
                UserId = userId
            };

            this.Critical(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
        }

        /// <summary>
        ///     Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Error(string message, string clientId, string deviceId, string requestId, string sessionId, string userId, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", Exception exception = null, Dictionary<string, object> payload = null)
        {
            TraceEntry traceEntry = new TraceEntry
            {
                ClientId = clientId,
                DeviceId = deviceId,
                RequestId = requestId,
                SessionId = sessionId,
                UserId = userId
            };

            this.Error(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
        }

        /// <summary>
        ///     Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Info(string message, string clientId, string deviceId, string requestId, string sessionId, string userId, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", Exception exception = null, Dictionary<string, object> payload = null)
        {
            TraceEntry traceEntry = new TraceEntry
            {
                ClientId = clientId,
                DeviceId = deviceId,
                RequestId = requestId,
                SessionId = sessionId,
                UserId = userId
            };

            this.Info(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
        }

        /// <summary>
        ///     Logs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Log(int level, string message, string clientId, string deviceId, string requestId, string sessionId, string userId, string tag = "None", ulong errorCode = 0, string errorCodeMessage = "", Exception exception = null, Dictionary<string, object> payload = null)
        {
            switch (level)
            {
                case 1:
                    this.Critical(message, clientId, deviceId, requestId, sessionId, userId, tag, errorCode, errorCodeMessage, exception, payload);
                    break;

                case 2:
                    this.Error(message, clientId, deviceId, requestId, sessionId, userId, tag, errorCode, errorCodeMessage, exception, payload);
                    break;

                case 3:
                    this.Warning(message, clientId, deviceId, requestId, sessionId, userId, tag, errorCode, errorCodeMessage, exception, payload);
                    break;

                case 4:
                    this.Info(message, clientId, deviceId, requestId, sessionId, userId, tag, errorCode, errorCodeMessage, exception, payload);
                    break;

                case 5:
                    this.Verbose(message, clientId, deviceId, requestId, sessionId, userId, tag, errorCode, errorCodeMessage, exception, payload);
                    break;

                default:
                    this.Verbose(message, clientId, deviceId, requestId, sessionId, userId, tag, errorCode, errorCodeMessage, exception, payload);
                    break;
            }
        }

        /// <summary>
        ///     Verboses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Verbose(string message, string clientId, string deviceId, string requestId, string sessionId, string userId, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", Exception exception = null, Dictionary<string, object> payload = null)
        {
            TraceEntry traceEntry = new TraceEntry
            {
                ClientId = clientId,
                DeviceId = deviceId,
                RequestId = requestId,
                SessionId = sessionId,
                UserId = userId
            };

            this.Verbose(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
        }

        /// <summary>
        ///     Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        public void Warning(string message, string clientId, string deviceId, string requestId, string sessionId, string userId, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", Exception exception = null, Dictionary<string, object> payload = null)
        {
            TraceEntry traceEntry = new TraceEntry
            {
                ClientId = clientId,
                DeviceId = deviceId,
                RequestId = requestId,
                SessionId = sessionId,
                UserId = userId
            };

            this.Warning(message, tag, errorCode, errorCodeMessage, traceEntry, exception, payload);
        }

        /// <summary>
        ///     Builds the log entry.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorCodeMessage">The error code message.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>LogEntry.</returns>
        protected static LogEntry BuildLogEntry(int level, string message, string tag = "None", ulong errorCode = 0UL, string errorCodeMessage = "", TraceEntry traceEntry = null, Exception exception = null, Dictionary<string, object> payload = null)
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendLine(message);

            if (exception != null)
            {
                messageBuilder.AppendLine(exception.GetExceptionString());
            }

            LogEntry logEntry = new LogEntry
            {
                DeploymentId = App.Host.DeploymentId.ToGuidString(),
                ErrorCode = errorCode,
                ErrorCodeMsg = errorCodeMessage,
                EventId = level.ToString(),
                Function = string.Empty,
                Level = level,
                PreciseTimeStamp = DateTime.UtcNow,
                Role = App.Host.Role,
                RoleInstance = App.Host.RoleInstance,
                Message = new MessageContent(traceEntry)
                {
                    ErrorCode = errorCode,
                    ErrorCodeMsg = errorCodeMessage,
                    Message = messageBuilder.ToString(),
                    Payload = payload ?? new Dictionary<string, object>(),
                    Tag = tag
                }
            };
            return logEntry;
        }

        /// <summary>
        ///     Initializes the application logger.
        /// </summary>
        /// <returns>NLog.ILogger.</returns>
        private static NLog.ILogger InitApplicationLogger()
        {
            return NLog.LogManager.GetLogger("ApplicationLogger");
        }
    }
}