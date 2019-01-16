// *****************************************************************************************************************
// Project          : NavyBlue.Framework
// File             : DemoConfig.cs
// Created          : 2019-01-14  14:21
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  15:11
// *****************************************************************************************************************
// <copyright file="DemoConfig.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.AspNetCore.Lib.Configs;
using System.Collections.Generic;

namespace NavyBlue.Demo.API
{
    /// <summary>
    /// 
    /// </summary>
    public class DemoConfig : IConfig
    {
        #region IConfig Members

        /// <summary>
        /// Gets or sets the bearer authentication keys.
        /// </summary>
        public string BearerAuthKeys { get; set; }

        /// <summary>
        /// Gets or sets the government server public key.
        /// </summary>
        public string GovernmentServerPublicKey { get; set; }

        /// <summary>
        /// Gets the ip whitelists.
        /// </summary>
        public List<string> IPWhitelists { get; }

        /// <summary>
        /// Gets the resources.
        /// </summary>
        public Dictionary<string, string> Resources { get; }

        #endregion IConfig Members
    }
}