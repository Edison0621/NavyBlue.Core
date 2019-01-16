// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IConfig.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:56
// *****************************************************************************************************************
// <copyright file="IConfig.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System.Collections.Generic;

namespace NavyBlue.AspNetCore.Lib.Configs
{
    /// <summary>
    ///     Interface IConfig
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        ///     Gets or sets the bearer authentication keys.
        /// </summary>
        string BearerAuthKeys { get; set; }

        /// <summary>
        ///     Gets or sets the government server public key.
        /// </summary>
        string GovernmentServerPublicKey { get; set; }

        /// <summary>
        ///     Gets the ip whitelists.
        /// </summary>
        /// <value>The ip whitelists.</value>
        List<string> IPWhitelists { get; }

        /// <summary>
        ///     Gets the resources.
        /// </summary>
        /// <value>The resources.</value>
        Dictionary<string, string> Resources { get; }
    }

    /// <summary>
    ///     Interface IConfig
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public interface IConfig<out TConfig> : IConfig where TConfig : class
    {
        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        TConfig Config { get; }
    }
}