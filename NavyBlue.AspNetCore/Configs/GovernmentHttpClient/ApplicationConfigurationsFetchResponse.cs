// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ApplicationConfigurationsFetchResponse.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:56
// *****************************************************************************************************************
// <copyright file="ApplicationConfigurationsFetchResponse.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System.Collections.Generic;

namespace NavyBlue.NetCore.Lib.Configs.GovernmentHttpClient
{
    /// <summary>
    ///     ApplicationConfigurationsFetchResponse.
    /// </summary>
    public class ApplicationConfigurationsFetchResponse
    {
        /// <summary>
        ///     Gets or sets the configurations.
        /// </summary>
        /// <value>The configurations.</value>
        public string Configurations { get; set; }

        /// <summary>
        ///     Gets or sets the configuration version.
        /// </summary>
        /// <value>The configuration version.</value>
        public string ConfigurationVersion { get; set; }

        /// <summary>
        ///     Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public Dictionary<string, KeyValuePair<string, string>> Permissions { get; set; }
    }
}