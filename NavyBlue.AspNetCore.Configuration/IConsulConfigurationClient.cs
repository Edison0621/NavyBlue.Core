// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IConsulConfigurationClient.cs
// Created          : 2019-01-29  13:34
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:27
// *****************************************************************************************************************
// <copyright file="IConsulConfigurationClient.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.Extensions.Primitives;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.ConsulConfiguration
{
    /// <summary>
    /// </summary>
    internal interface IConsulConfigurationClient
    {
        Task<QueryResult<KVPair>> GetConfig(string key, CancellationToken cancellationToken);

        IChangeToken Watch(string key, CancellationToken cancellationToken);
    }
}