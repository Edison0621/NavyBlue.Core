// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ConsulConfigurationClient.cs
// Created          : 2019-01-29  13:34
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:27
// *****************************************************************************************************************
// <copyright file="ConsulConfigurationClient.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NavyBlue.AspNetCore.ConsulConfiguration.Extensions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.ConsulConfiguration
{
    internal sealed class ConsulConfigurationClient : IConsulConfigurationClient
    {
        private readonly object lastIndexLock = new object();
        private readonly IConsulConfigurationSource consulConfigurationSource;
        private ulong lastIndex;
        private ConfigurationReloadToken reloadToken = new ConfigurationReloadToken();

        public ConsulConfigurationClient(IConsulConfigurationSource consulConfigurationSource)
        {
            this.consulConfigurationSource = consulConfigurationSource;
        }

        #region IConsulConfigurationClient Members

        public async Task<QueryResult<KVPair>> GetConfig(string key, CancellationToken cancellationToken)
        {
            QueryResult<KVPair> result = await this.GetKvPairs(key, cancellationToken).ConfigureAwait(false);
            this.UpdateLastIndex(result);

            return result;
        }

        public IChangeToken Watch(string key, CancellationToken cancellationToken)
        {
            Task.Run(() => this.RefreshForChanges(key, cancellationToken));

            return this.reloadToken;
        }

        #endregion IConsulConfigurationClient Members

        private async Task<QueryResult<KVPair>> GetKvPairs(string key, CancellationToken cancellationToken, QueryOptions queryOptions = null)
        {
            using (IConsulClient consulClient = this.consulConfigurationSource.CreateConsulClient())
            {
                QueryResult<KVPair> result = await consulClient.KV.Get(key, queryOptions, cancellationToken).ConfigureAwait(false);

                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NotFound:
                        return result;

                    default:
                        throw new Exception($"加载配置时出错，HTTP状态码: {result.StatusCode}.");
                }
            }
        }

        private async Task<bool> HasValueChanged(string key, CancellationToken cancellationToken)
        {
            QueryOptions queryOptions;
            lock (this.lastIndexLock)
            {
                queryOptions = new QueryOptions
                {
                    WaitIndex = this.lastIndex
                };
            }

            QueryResult<KVPair> result = await this.GetKvPairs(key, cancellationToken, queryOptions).ConfigureAwait(false);

            return result != null && this.UpdateLastIndex(result);
        }

        private async Task RefreshForChanges(string key, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (await this.HasValueChanged(key, cancellationToken).ConfigureAwait(false))
                    {
                        ConfigurationReloadToken previousToken = Interlocked.Exchange(ref this.reloadToken, new ConfigurationReloadToken());
                        previousToken.OnReload();

                        return;
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        private bool UpdateLastIndex(QueryResult queryResult)
        {
            lock (this.lastIndexLock)
            {
                if (queryResult.LastIndex > this.lastIndex)
                {
                    this.lastIndex = queryResult.LastIndex;
                    return true;
                }
            }

            return false;
        }
    }
}