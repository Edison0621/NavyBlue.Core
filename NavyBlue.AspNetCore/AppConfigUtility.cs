// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AppSettings.cs
// Created          : 2019-01-25  13:10
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-29  13:34
// *****************************************************************************************************************
// <copyright file="AppSettings.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.Extensions.Configuration;
using System;

namespace NavyBlue.AspNetCore
{
    public class AppConfigUtility
    {
        public static IDisposable CallbackRegistration;

        public static IConfigurationRoot ConfigManager { get; set; }

        public static T Get<T>(string sectionKey)
        {
            return ConfigManager.GetSection(sectionKey).Get<T>();
        }

        public static void OnSettingChanged(object state)
        {
            CallbackRegistration?.Dispose();
            ConfigManager = (IConfigurationRoot)state;
            CallbackRegistration = ConfigManager.GetReloadToken().RegisterChangeCallback(OnSettingChanged, state);
        }
    }
}