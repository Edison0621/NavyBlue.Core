// *****************************************************************************************************************
// Project          : NavyBlue
// File             : Program.cs
// Created          : 2019-01-10  18:53
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  19:25
// *****************************************************************************************************************
// <copyright file="Program.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace NavyBlue.Demo.API
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
    }
}