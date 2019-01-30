// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AppBuilderExtensions.cs
// Created          : 2019-01-24  16:00
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-24  16:28
// *****************************************************************************************************************
// <copyright file="AppBuilderExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace NavyBlue.ServiceGovern
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            ServiceRegister.Register(lifetime);

            return app;
        }
    }
}