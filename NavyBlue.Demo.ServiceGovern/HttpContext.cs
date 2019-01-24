// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HttpContext.cs
// Created          : 2019-01-23  15:29
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-23  15:36
// *****************************************************************************************************************
// <copyright file="HttpContext.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NavyBlue.Demo.ServiceGovern
{
    public class HttpContext
    {
        public static IServiceProvider ServiceProvider;

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService<IHttpContextAccessor>();

                Microsoft.AspNetCore.Http.HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
    }
}