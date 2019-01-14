// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NBHttpContext.cs
// Created          : 2019-01-14  19:37
// 
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  19:37
// *****************************************************************************************************************
// <copyright file="NBHttpContext.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************


using Microsoft.AspNetCore.Http;
using System;

namespace NavyBlue.NetCore.Lib
{
    public class NBHttpContext
    {
        public static IServiceProvider ServiceProvider;

        static NBHttpContext()
        { }


        public static HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor));

                HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
    }
}