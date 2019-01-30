// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ConfigurationBuilderExtensions.cs
// Created          : 2019-01-29  15:43
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:26
// *****************************************************************************************************************
// <copyright file="ConfigurationBuilderExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace NavyBlue.AspNetCore.ConsulConfiguration.Extensions
{
    /// <summary>
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, string key, CancellationToken cancellationToken)
        {
            return builder.AddConsul(key, cancellationToken, options => { });
        }

        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, string key, CancellationToken cancellationToken, Action<IConsulConfigurationSource> options)
        {
            var consulConfigSource = new ConsulConfigurationSource(key, cancellationToken);
            options(consulConfigSource);
            return builder.Add(consulConfigSource);
        }
    }
}