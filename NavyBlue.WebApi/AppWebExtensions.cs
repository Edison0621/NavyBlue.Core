// ***********************************************************************
// Project          : MoeLib
// File             : AppWebExtensions.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  10:10 AM
// ***********************************************************************
// <copyright file="AppWebExtensions.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using NavyBlue.Lib.Jinyinmao;
using NavyBlue.AspNetCore.Web.Diagnostics;
using NavyBlue.AspNetCore.Web.Web.Diagnostics;

namespace NavyBlue.AspNetCore.Web.Web
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