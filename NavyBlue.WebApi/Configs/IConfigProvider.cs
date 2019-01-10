// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IConfigProvider.cs
// Created          : 2019-01-10  11:12
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  14:59
// *****************************************************************************************************************
// <copyright file="IConfigProvider.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;

namespace NavyBlue.AspNetCore.Web.Configs
{
    /// <summary>
    ///     Interface IConfigProvider
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        ///     Gets the type of the configuration.
        /// </summary>
        /// <returns>Type.</returns>
        Type GetConfigType();

        /// <summary>
        ///     Gets the configurations string.
        /// </summary>
        /// <returns>System.String.</returns>
        SourceConfig GetSourceConfig();
    }
}