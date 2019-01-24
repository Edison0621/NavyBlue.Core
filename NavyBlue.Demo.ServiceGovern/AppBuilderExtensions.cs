using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace NavyBlue.Demo.ServiceGovern
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, ServiceRegisterEntity serviceEntity)
        {
            var consulClient = new ConsulClient(x => x.Address = new Uri(serviceEntity.ConsulServer)); //请求注册的 Consul 地址
            string address = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(p => p.AddressFamily == AddressFamily.InterNetwork).ToString();

            string httpScheme = serviceEntity.IsHttps ? "https://" : "http://";

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), //服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10), //心跳间隔
                HTTP = $"{httpScheme}{address}:{serviceEntity.ServicePort}/api/health", //健康检查地址
                Timeout = TimeSpan.FromSeconds(5)//超时时间
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = serviceEntity.ServiceName,
                Address = $"{httpScheme}{address}",
                Port = serviceEntity.ServicePort,
                Tags = new[] { $"urlprefix-/{serviceEntity.ServiceName}" } //添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            //服务启动时注册
            consulClient.Agent.ServiceRegister(registration).Wait();
            lifetime.ApplicationStopping.Register(() =>
            {
                //服务停止时取消注册
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            //string conn = consulClient.KV.Get("govern").ConfigureAwait(false).GetAwaiter().GetResult();

            return app;
        }
    }
}