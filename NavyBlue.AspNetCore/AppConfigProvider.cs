﻿// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AppConfigProvider.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:12
// *****************************************************************************************************************
// <copyright file="AppConfigProvider.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;

namespace NavyBlue.NetCore.Lib
{
    /// <summary>
    ///     AppConfigProvider.
    /// </summary>
    public class AppConfigProvider : IAppConfigProvider
    {
        #region IAppConfigProvider Members

        /// <summary>
        ///     Gets the application keys configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetAppKeysConfig()
        {
            string config = ConfigurationManager.AppSettings.Get("AppKeys");
            if (config.IsNullOrEmpty())
            {
                throw new Exception("Missing config of \"AppKeys\"");
            }

            return config.HtmlDecode();
        }

        /// <summary>
        ///     Gets the deployment identifier configuration.
        /// </summary>
        /// <returns>Guid.</returns>
        public virtual Guid GetDeploymentIdConfig()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        ///     Gets the environment.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetEnvironmentConfig()
        {
            string config = ConfigurationManager.AppSettings.Get("Env");
            return config.IsNullOrEmpty() ? "DEV" : config;
        }

        /// <summary>
        ///     Gets the role configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetRoleConfig()
        {
            string config = ConfigurationManager.AppSettings.Get("Role");
            if (config.IsNullOrEmpty())
            {
                throw new Exception("Missing config of \"Role\"");
            }

            return config;
        }

        /// <summary>
        ///     Gets the role instance configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetRoleInstanceConfig()
        {
            return this.GetRoleConfig() + "_IN_" + HostServer.IP;
        }

        #endregion IAppConfigProvider Members
    }
}