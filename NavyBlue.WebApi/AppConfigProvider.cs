// ***********************************************************************
// Project          : MoeLib
// File             : AppConfigProvider.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  12:43 PM
// ***********************************************************************
// <copyright file="AppConfigProvider.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Configuration;
using Moe.Lib;

namespace NavyBlue.AspNetCore.Web
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
                throw new ConfigurationErrorsException("Missing config of \"AppKeys\"");
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
                throw new ConfigurationErrorsException("Missing config of \"Role\"");
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