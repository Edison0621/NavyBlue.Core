using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using NavyBlue.AspNetCore.ConsulConfiguration.Extensions;

namespace NavyBlue.Demo.ServiceGovern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration(
                (hostingContext, builder) =>
                {
                    builder.AddConsul("userservice", cancellationTokenSource.Token, options =>
                    {
                        options.ConsulConfigurationOptions = cco => { cco.Address = new Uri("http://localhost:8500"); };
                        options.Optional = true;
                        options.ReloadOnChange = true;
                    }).AddEnvironmentVariables();

                    //builder.AddConsul("commonservice", cancellationTokenSource.Token, options =>
                    //{
                    //    options.ConsulConfigurationOptions = cco => { cco.Address = new Uri("http://localhost:8500"); };
                    //    options.Optional = true;
                    //    options.ReloadOnChange = true;
                    //}).AddEnvironmentVariables();
                }).UseStartup<Startup>().Build().Run();
        }
    }
}