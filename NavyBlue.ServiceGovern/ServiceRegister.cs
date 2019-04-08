// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ServiceRegister.cs
// Created          : 2019-01-24  16:26
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-24  16:28
// *****************************************************************************************************************
// <copyright file="ServiceRegister.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.AspNetCore.Hosting;
using NavyBlue.AspNetCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;

namespace NavyBlue.ServiceGovern
{
    internal class ServiceRegister
    {
        internal static void Register(IApplicationLifetime lifetime)
        {
            ServiceRegisterInfo serviceInfo = AppConfigUtility.Get<ServiceRegisterInfo>("ServiceRegisterEntity");

            using (var consulClient = new ConsulClient(x => x.Address = new Uri(serviceInfo.ConsulServer)))
            {
                string address = GetAddress(serviceInfo);
                string httpScheme = serviceInfo.IsHttps ? "https://" : "http://";

                AgentServiceRegistration registration = BuildServiceRegistration(serviceInfo, "localhost", httpScheme);

                consulClient.Agent.ServiceRegister(registration).Wait();

                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();
                });

                var result = consulClient.Agent.Services().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        /// <summary>
        /// Fabio是一个快速、现代、zero-conf负载均衡HTTP(S)路由器,用于部署Consul管理的微服务
        /// 添加urlprefix-/servicename格式的tag标签，可以方便Fabio识别
        /// </summary>
        /// <param name="serviceinfo">The serviceinfo.</param>
        /// <param name="address">The address.</param>
        /// <param name="httpScheme">The HTTP scheme.</param>
        /// <returns></returns>
        private static AgentServiceRegistration BuildServiceRegistration(ServiceRegisterInfo serviceinfo, string address, string httpScheme)
        {
            serviceinfo.HealthCheck.HTTP = $"{httpScheme}{address}:{serviceinfo.ServicePort}/{serviceinfo.HealthCheck.HTTP}";

            return new AgentServiceRegistration
            {
                Checks = new[] { serviceinfo.HealthCheck },
                ID = Guid.NewGuid().ToString(),
                Name = serviceinfo.ServiceName,
                Address = $"{httpScheme}{address}",
                Port = serviceinfo.ServicePort,
                Tags = new[] { $"urlprefix-/{serviceinfo.ServiceName}" } 
            };
        }

        private static string GetAddress(ServiceRegisterInfo serviceInfo)
        {
            Expression<Func<IPAddress, bool>> expression;
            if (serviceInfo.IsIPV6)
            {
                expression = p => p.AddressFamily == AddressFamily.InterNetworkV6;
            }
            else
            {
                expression = p => p.AddressFamily == AddressFamily.InterNetwork;
            }

            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.AsQueryable().FirstOrDefault(expression).ToString();
        }
    }
}