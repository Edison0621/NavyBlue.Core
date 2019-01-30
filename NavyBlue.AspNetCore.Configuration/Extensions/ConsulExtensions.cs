// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ConsulExtensions.cs
// Created          : 2019-01-29  14:35
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:26
// *****************************************************************************************************************
// <copyright file="ConsulExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;

namespace NavyBlue.AspNetCore.Configuration.Extensions
{
    internal static class ConsulExtensions
    {
        public static ConsulClient CreateConsulClient(this IConsulConfigurationSource _consulConfigSource)
        {
            return new ConsulClient(_consulConfigSource.ConsulConfigurationOptions, 
                _consulConfigSource.ConsulHttpClientOptions,
                _consulConfigSource.ConsulHttpClientHandlerOptions);
        }
    }
}