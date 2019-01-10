// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AppWebExtensions.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:02
// *****************************************************************************************************************
// <copyright file="AppWebExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.AspNetCore.Web.Diagnostics;

namespace NavyBlue.AspNetCore.Web
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