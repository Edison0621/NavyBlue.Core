// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ConfigManager.cs
// Created          : 2019-01-10  11:12
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  14:59
// *****************************************************************************************************************
// <copyright file="ConfigManager.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Configs
{
    /// <summary>
    ///     ConfigManager.
    /// </summary>
    public class ConfigManager
    {
        private readonly object _lock = new object();
        private readonly IConfigProvider configProvider;
        private bool isConfigRefreshing;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigManager" /> class.
        /// </summary>
        /// <param name="configProvider">The configuration provider.</param>
        internal ConfigManager(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
            this.RefreshInterval = 5.Minutes();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigManager" /> class.
        /// </summary>
        /// <param name="configProvider">The configuration provider.</param>
        /// <param name="refreshInterval">The refresh interval.</param>
        internal ConfigManager(IConfigProvider configProvider, TimeSpan refreshInterval)
        {
            this.configProvider = configProvider;
            this.RefreshInterval = refreshInterval;
        }

        /// <summary>
        ///     Gets or sets the configuration refresh time.
        /// </summary>
        /// <value>The configuration refresh time.</value>
        public DateTime ConfigRefreshTime { get; protected set; }

        /// <summary>
        ///     Gets the ip whitelists.
        /// </summary>
        /// <value>The ip whitelists.</value>
        public List<string> IPWhitelists
        {
            get { return this.GetIPWhitelists(); }
        }

        /// <summary>
        ///     Gets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public Dictionary<string, KeyValuePair<string, string>> Permissions
        {
            get { return this.GetPermissions(); }
        }

        /// <summary>
        ///     Gets or sets the refresh interval.
        /// </summary>
        /// <value>The refresh interval.</value>
        public TimeSpan RefreshInterval { get; }

        /// <summary>
        ///     Gets the resources.
        /// </summary>
        /// <value>The resources.</value>
        public Dictionary<string, string> Resources
        {
            get { return this.GetResources(); }
        }

        /// <summary>
        ///     Gets or sets the source configuration.
        /// </summary>
        /// <value>The source configuration.</value>
        public SourceConfig SourceConfig { get; set; }

        private IConfig Config { get; set; }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <returns>TConfig.</returns>
        public TConfig GetConfig<TConfig>() where TConfig : class, IConfig
        {
            if (!this.GetConfigType().IsEquivalentTo(typeof(TConfig)))
            {
                throw new InvalidOperationException($"The config type {typeof(TConfig)} is incorrect.");
            }

            if (this.Config == null)
            {
                return this.GetSourceConfig().Configurations.FromJson<TConfig>();
            }

            return (TConfig)this.Config;
        }

        /// <summary>
        ///     Gets the type of the configuration.
        /// </summary>
        /// <returns>Type.</returns>
        public Type GetConfigType()
        {
            return this.configProvider.GetConfigType();
        }

        /// <summary>
        ///     Gets the configurations string.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetConfigurationsString()
        {
            return this.GetSourceConfig().Configurations;
        }

        /// <summary>
        ///     Gets the get configuration version.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetConfigurationVersion()
        {
            return this.SourceConfig == null ? "init" : this.SourceConfig.ConfigurationVersion;
        }

        /// <summary>
        ///     Gets the ip whitelists.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetIPWhitelists()
        {
            if (this.Config == null)
            {
                return this.GetSourceConfig().Configurations.FromJson<NavyBlueConfig>().IPWhitelists;
            }

            return this.Config.IPWhitelists;
        }

        /// <summary>
        ///     Gets the permission.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>System.Nullable&lt;KeyValuePair&lt;System.String, System.String&gt;&gt;.</returns>
        public KeyValuePair<string, string>? GetPermission(string serviceName)
        {
            if (this.Permissions.ContainsKey(serviceName))
            {
                return this.Permissions[serviceName];
            }

            return null;
        }

        /// <summary>
        ///     Gets the permissions.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, KeyValuePair&lt;System.String, System.String&gt;&gt;.</returns>
        public Dictionary<string, KeyValuePair<string, string>> GetPermissions()
        {
            return this.GetSourceConfig().Permissions;
        }

        /// <summary>
        ///     Gets the resources.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public Dictionary<string, string> GetResources()
        {
            if (this.Config == null)
            {
                return this.GetSourceConfig().Configurations.FromJson<NavyBlueConfig>().Resources;
            }

            return this.Config.Resources;
        }

        /// <summary>
        ///     Gets the resource string.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>System.String.</returns>
        public string GetResourceString(string resourceName)
        {
            string resourceString;
            this.Resources.TryGetValue(resourceName, out resourceString);
            return resourceString;
        }

        /// <summary>
        ///     Determines whether [is configuration need refresh].
        /// </summary>
        /// <returns><c>true</c> if [is configuration need refresh]; otherwise, <c>false</c>.</returns>
        protected bool IsConfigNeedRefresh()
        {
            return this.ConfigRefreshTime.Add(this.RefreshInterval) < DateTime.UtcNow;
        }

        private SourceConfig GetSourceConfig()
        {
            if (this.SourceConfig == null)
            {
                this.RefreshConfig();
            }
            else
            {
                if (this.IsConfigNeedRefresh())
                {
                    Task.Run(() => this.RefreshConfig());
                }
            }

            return this.SourceConfig;
        }

        private void RefreshConfig()
        {
            if (!this.isConfigRefreshing)
            {
                try
                {
                    lock (this._lock)
                    {
                        if (!this.isConfigRefreshing)
                        {
                            this.isConfigRefreshing = true;
                            this.SourceConfig = this.configProvider.GetSourceConfig();
                            this.Config = (IConfig)JsonConvert.DeserializeObject(this.SourceConfig.Configurations, this.GetConfigType());
                            this.ConfigRefreshTime = DateTime.UtcNow;
                        }
                    }
                }
                catch (Exception e)
                {
                    App.LogManager.CreateLogger().Critical("Can not refresh configurations!", "CONFIGURATIONS ERROR", 0UL, e.Message, null, e,
                        new Dictionary<string, object>
                        {
                            { "SourceVersion", this.GetConfigurationVersion() }
                        });

                    if (this.SourceConfig == null)
                    {
                        throw new ConfigurationErrorsException("Can not init configurations!", e);
                    }
                }
                finally
                {
                    this.isConfigRefreshing = false;
                }
            }
        }
    }
}