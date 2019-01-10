// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NavyBlueConfig.cs
// Created          : 2019-01-10  11:12
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  14:59
// *****************************************************************************************************************
// <copyright file="NavyBlueConfig.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System.Collections.Generic;

namespace NavyBlue.AspNetCore.Web.Configs
{
    internal class NavyBlueConfig : IConfig
    {
        #region IConfig Members

        /// <summary>
        ///     Gets the ip whitelists.
        /// </summary>
        /// <value>The ip whitelists.</value>
        public List<string> IPWhitelists { get; set; }

        /// <summary>
        ///     Gets the resources.
        /// </summary>
        /// <value>The resources.</value>
        public Dictionary<string, string> Resources { get; set; }

        #endregion IConfig Members
    }
}