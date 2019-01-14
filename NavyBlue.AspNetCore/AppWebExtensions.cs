// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AppWebExtensions.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:12
// *****************************************************************************************************************
// <copyright file="AppWebExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.NetCore.Lib.Loggers;

namespace NavyBlue.NetCore.Lib
{
    /// <summary>
    ///     AppWebExtensions.
    /// </summary>
    public static class AppWebExtensions
    {
        /// <summary>
        ///     Creates the web logger.
        /// </summary>
        /// <param name="logManager">The log manager.</param>
        /// <returns>IWebLogger.</returns>
        public static IWebLogger CreateWebLogger(this LogManager logManager)
        {
            return App.IsInAzureCloud ? (IWebLogger)new WADWebLogger() : new NWebLogger();
        }
    }
}