// ***********************************************************************
// Project          : MoeLib
// File             : LogManager.cs
// Created          : 2015-11-23  5:22 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  10:11 AM
// ***********************************************************************
// <copyright file="LogManager.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

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