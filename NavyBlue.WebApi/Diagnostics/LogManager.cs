// *****************************************************************************************************************
// Project          : NavyBlue
// File             : LogManager.cs
// Created          : 2019-01-10  9:56
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:00
// *****************************************************************************************************************
// <copyright file="LogManager.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using MoeLib.Diagnostics;

namespace NavyBlue.AspNetCore.Web.Diagnostics
{
    /// <summary>
    ///     LogManager.
    /// </summary>
    public class LogManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LogManager" /> class.
        /// </summary>
        internal LogManager()
        {
        }

        /// <summary>
        ///     Creates the logger.
        /// </summary>
        /// <returns>ILogger.</returns>
        public ILogger CreateLogger()
        {
            return App.IsInAzureCloud ? (ILogger)new WADLogger() : new NLogger();
        }
    }
}