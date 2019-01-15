// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AppExtension.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:57
// *****************************************************************************************************************
// <copyright file="AppExtension.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.Extensions.Options;
using NavyBlue.AspNetCore;

namespace NavyBlue.NetCore.Lib
{
    /// <summary>
    ///     AppAzureExtension.
    /// </summary>
    public static class AppExtension
    {
        /// <summary>
        ///     Initializes the configuration.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static App InitConfig(this App app,IOptions<AppConfig> appConfig)
        {
            return app.Config(new AppConfigProvider(appConfig));
        }
    }
}