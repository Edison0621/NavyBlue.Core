// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NBConfig.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:56
// *****************************************************************************************************************
// <copyright file="NBConfig.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System.Collections.Generic;

namespace NavyBlue.NetCore.Lib.Configs
{
    /// <summary>
    /// </summary>
    /// <seealso cref="NavyBlue.NetCore.Lib.Configs.IConfig" />
    internal class NBConfig : IConfig
    {
        #region IConfig Members

        /// <summary>
        ///     Gets or sets the bearer authentication keys.
        /// </summary>
        public string BearerAuthKeys { get; set; }

        /// <summary>
        ///     Gets or sets the government server public key.
        /// </summary>
        public string GovernmentServerPublicKey { get; set; }

        /// <summary>
        ///     Gets the ip whitelists.
        /// </summary>
        /// <value>
        ///     The ip whitelists.
        /// </value>
        public List<string> IPWhitelists { get; set; }

        /// <summary>
        ///     Gets the resources.
        /// </summary>
        /// <value>
        ///     The resources.
        /// </value>
        public Dictionary<string, string> Resources { get; set; }

        #endregion IConfig Members
    }
}