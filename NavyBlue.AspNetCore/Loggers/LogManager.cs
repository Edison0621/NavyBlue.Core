// *****************************************************************************************************************
// Project          : NavyBlue
// File             : LogManager.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:12
// *****************************************************************************************************************
// <copyright file="LogManager.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

namespace NavyBlue.NetCore.Lib.Loggers
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