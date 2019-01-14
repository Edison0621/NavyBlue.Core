// *****************************************************************************************************************
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NavyBlue.AspNetCore;

namespace NavyBlue.NetCore.Lib
{
    /// <summary>
    ///     AppConfigProvider.
    /// </summary>
    public class AppConfigProvider : IAppConfigProvider
    {
        private readonly IOptions<AppConfig> appConfig = NBHttpContext.Current.RequestServices.GetService<IOptions<AppConfig>>();

        #region IAppConfigProvider Members

        /// <summary>
        ///     Gets the application keys configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetAppKeysConfig()
        {
            if (appConfig.Value.AppKeys.IsNullOrEmpty())
            {
                throw new Exception("Missing config of \"AppKeys\"");
            }

            return appConfig.Value.AppKeys.HtmlDecode();
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
            return appConfig.Value.Env.IsNullOrEmpty() ? "DEV" : this.appConfig.Value.Env;
        }

        /// <summary>
        ///     Gets the role configuration.
        /// </summary>
        /// <returns>System.String.</returns>
        public virtual string GetRoleConfig()
        {
            if (appConfig.Value.Role.IsNullOrEmpty())
            {
                throw new Exception("Missing config of \"Role\"");
            }

            return appConfig.Value.Role;
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