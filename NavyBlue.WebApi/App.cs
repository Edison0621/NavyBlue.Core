// ***********************************************************************
// Project          : MoeLib
// File             : App.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  10:11 AM
// ***********************************************************************
// <copyright file="App.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Configuration;
using NavyBlue.AspNetCore.Web;
using NavyBlue.AspNetCore.Web.Configs;
using NavyBlue.AspNetCore.Web.Diagnostics;
using NavyBlue.Lib;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     App.
    /// </summary>
    public class App
    {
        /// <summary>
        ///     The application
        /// </summary>
        private static readonly App app = new App();

        /// <summary>
        ///     The configurations
        /// </summary>
        private ConfigManager configurations;

        /// <summary>
        ///     The host
        /// </summary>
        private Host host;

        /// <summary>
        ///     The log manager
        /// </summary>
        private LogManager logManager;

        /// <summary>
        ///     Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        private App()
        {
        }

        /// <summary>
        ///     Gets the condigurations.
        /// </summary>
        /// <value>The condigurations.</value>
        /// <exception cref="System.InvalidOperationException">The app has not configurated the ConfigManager.</exception>
        public static ConfigManager Configurations
        {
            get
            {
                if (!app.Initialized || !app.Configurated)
                {
                    ThrowInvalidOperationException();
                }

                if (app.configurations == null)
                {
                    throw new InvalidOperationException("The app has not configurated the ConfigManager.");
                }

                return app.configurations;
            }
        }

        /// <summary>
        ///     Gets the host.
        /// </summary>
        /// <value>The host.</value>
        public static Host Host
        {
            get
            {
                if (!app.Initialized || !app.Configurated)
                {
                    ThrowInvalidOperationException();
                }
                return app.host;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is in azure cloud.
        /// </summary>
        /// <value><c>true</c> if this instance is in azure cloud; otherwise, <c>false</c>.</value>
        public static bool IsInAzureCloud
        {
            get { return app.host.IsInAzureCloud(); }
        }

        /// <summary>
        ///     Gets the log manager.
        /// </summary>
        /// <value>The log manager.</value>
        public static LogManager LogManager
        {
            get
            {
                if (!app.Initialized || !app.Configurated)
                {
                    ThrowInvalidOperationException();
                }
                return app.logManager;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="App" /> is configurated.
        /// </summary>
        /// <value><c>true</c> if configurated; otherwise, <c>false</c>.</value>
        public bool Configurated { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="App" /> is initialized.
        /// </summary>
        /// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
        public bool Initialized { get; private set; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        /// <returns>App.</returns>
        public static App Initialize()
        {
            app.host = new Host();
            app.logManager = new LogManager();

            app.configurations = null;

            app.Initialized = true;
            return app;
        }

        /// <summary>
        ///     Configurations the specified application configuration provider.
        /// </summary>
        /// <param name="appConfigProvider">The application configuration provider.</param>
        /// <returns>App.</returns>
        public App Config(IAppConfigProvider appConfigProvider)
        {
            this.host.appKeys = new Lazy<string>(() => appConfigProvider.GetAppKeysConfig());
            this.host.deploymentId = new Lazy<Guid>(() => appConfigProvider.GetDeploymentIdConfig());
            this.host.environment = new Lazy<string>(() => appConfigProvider.GetEnvironmentConfig());
            this.host.role = new Lazy<string>(() => appConfigProvider.GetRoleConfig());
            this.host.roleInstance = new Lazy<string>(() => appConfigProvider.GetRoleInstanceConfig());

            this.Configurated = true;

            return this;
        }

        /// <summary>
        ///     Configurations this instance.
        /// </summary>
        /// <returns>App.</returns>
        public App Config()
        {
            return this.Config(new AppConfigProvider());
        }

        /// <summary>
        ///     Uses the configuration manager.
        /// </summary>
        /// <param name="configProvider">The configuration provider.</param>
        /// <param name="configProviderForDev">The configuration provider for dev.</param>
        /// <returns>App.</returns>
        public App UseConfigManager(IConfigProvider configProvider, IConfigProvider configProviderForDev = null)
        {
            if (ConfigurationManager.AppSettings.Get("UseConfigProviderForDev").AsBoolean(false) && configProviderForDev != null)
            {
                app.configurations = new ConfigManager(configProviderForDev);
            }

            app.configurations = new ConfigManager(configProvider);

            return app;
        }

        /// <summary>
        ///     Uses the government server configuration manager.
        /// </summary>
        /// <typeparam name="TConfig">The type of the configuration.</typeparam>
        /// <param name="configProviderForDev">The configuration provider for dev.</param>
        /// <returns>App.</returns>
        public App UseGovernmentServerConfigManager<TConfig>(IConfigProvider configProviderForDev = null) where TConfig : class, IConfig
        {
            return this.UseConfigManager(new GovernmentServerConfigProvider<TConfig>(), configProviderForDev);
        }

        /// <summary>
        ///     Throws the invalid operation exception.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        ///     The App instance has not been initialized.
        ///     or
        ///     The App instance has not been configurated.
        /// </exception>
        private static void ThrowInvalidOperationException()
        {
            if (!app.Initialized)
            {
                throw new InvalidOperationException("The App instance has not been initialized.");
            }

            if (!app.Configurated)
            {
                throw new InvalidOperationException("The App instance has not been configurated.");
            }
        }
    }
}