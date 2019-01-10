// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IAppConfigProvider.cs
// Created          : 2019-01-10  11:11
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:03
// *****************************************************************************************************************
// <copyright file="IAppConfigProvider.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

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