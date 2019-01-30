// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ConsulConfigurationProvider.cs
// Created          : 2019-01-29  13:34
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:27
// *****************************************************************************************************************
// <copyright file="ConsulConfigurationProvider.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NavyBlue.AspNetCore.ConsulConfiguration.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.ConsulConfiguration
{
    internal sealed class ConsulConfigurationProvider : ConfigurationProvider
    {
        private readonly IConsulConfigurationClient consulConfigClient;
        private readonly IConsulConfigurationSource source;
        private readonly string serviceKey;

        public ConsulConfigurationProvider(IConsulConfigurationSource source, IConsulConfigurationClient consulConfigClient, string serviceKey)
        {
            this.consulConfigClient = consulConfigClient;
            this.source = source;
            this.serviceKey = serviceKey;

            if (source.ReloadOnChange)
            {
                ChangeToken.OnChange(
                    () => this.consulConfigClient.Watch(this.source.Key, this.source.CancellationToken),
                    async () =>
                    {
                        await this.DoLoad(true).ConfigureAwait(false);

                        this.OnReload();
                    });
            }
        }

        public override void Load()
        {
            try
            {
                this.DoLoad(false).Wait();
            }
            catch (AggregateException aggregateException)
            {
                throw aggregateException.InnerException;
            }
        }

        private async Task DoLoad(bool reloading)
        {
            try
            {
                QueryResult<KVPair> result = await this.consulConfigClient.GetConfig(this.source.Key, this.source.CancellationToken).ConfigureAwait(false);
                if ((result == null || result.Response == null) && !this.source.Optional)
                {
                    if (!reloading)
                    {
                        throw new Exception($"无法找到配置KEY{this.source.Key}.");
                    }

                    return;
                }

                var tt = result?.Response?.ToConfig();

                this.Data = new Dictionary<string, string> { { this.serviceKey, result?.Response?.ToConfig() } };
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}