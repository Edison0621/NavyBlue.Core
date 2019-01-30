// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ConsulConfigurationSource.cs
// Created          : 2019-01-29  13:34
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:27
// *****************************************************************************************************************
// <copyright file="ConsulConfigurationSource.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading;

namespace NavyBlue.AspNetCore.ConsulConfiguration
{
    internal sealed class ConsulConfigurationSource : IConsulConfigurationSource
    {
        public ConsulConfigurationSource(string key, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.Key = key;
            this.CancellationToken = cancellationToken;
        }

        #region IConsulConfigurationSource Members

        public CancellationToken CancellationToken { get; }

        public Action<ConsulClientConfiguration> ConsulConfigurationOptions { get; set; }

        public Action<HttpClientHandler> ConsulHttpClientHandlerOptions { get; set; }

        public Action<HttpClient> ConsulHttpClientOptions { get; set; }

        public string Key { get; }

        public bool Optional { get; set; } = false;

        public bool ReloadOnChange { get; set; } = false;

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var consulConfigClient = new ConsulConfigurationClient(this);

            return new ConsulConfigurationProvider(this, consulConfigClient, this.Key);
        }

        #endregion IConsulConfigurationSource Members
    }
}