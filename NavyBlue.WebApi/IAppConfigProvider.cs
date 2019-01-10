// ***********************************************************************
// Project          : MoeLib
// File             : IAppConfigProvider.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  12:36 PM
// ***********************************************************************
// <copyright file="IAppConfigProvider.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     Interface IAppConfigProvider
    /// </summary>
    public interface IAppConfigProvider
    {
        /// <summary>
        ///     Gets the application keys configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetAppKeysConfig();

        /// <summary>
        ///     Gets the deployment identifier configuration.
        /// </summary>
        /// <returns>Guid.</returns>
        Guid GetDeploymentIdConfig();

        /// <summary>
        ///     Gets the environment configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetEnvironmentConfig();

        /// <summary>
        ///     Gets the role configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetRoleConfig();

        /// <summary>
        ///     Gets the role instance configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetRoleInstanceConfig();
    }
}