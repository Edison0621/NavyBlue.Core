// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IConsulConfigurationSource.cs
// Created          : 2019-01-29  13:34
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:27
// *****************************************************************************************************************
// <copyright file="IConsulConfigurationSource.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading;

namespace NavyBlue.AspNetCore.Configuration
{
    public interface IConsulConfigurationSource : IConfigurationSource
    {
        CancellationToken CancellationToken { get; }

        Action<ConsulClientConfiguration> ConsulConfigurationOptions { get; set; }

        Action<HttpClientHandler> ConsulHttpClientHandlerOptions { get; set; }

        Action<HttpClient> ConsulHttpClientOptions { get; set; }

        string Key { get; }

        bool Optional { get; set; }

        bool ReloadOnChange { get; set; }
    }
}