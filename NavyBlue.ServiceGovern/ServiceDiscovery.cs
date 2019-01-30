// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ServiceDiscovery.cs
// Created          : 2019-01-24  16:25
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-24  17:02
// *****************************************************************************************************************
// <copyright file="ServiceDiscovery.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavyBlue.ServiceGovern
{
    public class ServiceDiscovery
    {
        private readonly ServiceRegisterInfo serviceInfo;

        public ServiceDiscovery(IOptions<ServiceRegisterInfo> options)
        {
            this.serviceInfo = options.Value;
        }

        public async Task<AgentService> GetAgentService(string serviceName)
        {
            var serviceList = await this.GetAgentServiceList();

            var service = serviceList.FirstOrDefault(p => p.Service == serviceName);

            if (service == null)
            {
                throw new Exception($"无法找到指定的服务,服务名{serviceName}");
            }

            return service;
        }

        public async Task<Dictionary<string, AgentService>> GetAgentServiceDictionary()
        {
            using (var consulClient = new ConsulClient(x => x.Address = new Uri(this.serviceInfo.ConsulServer)))
            {
                var result = await consulClient.Agent.Services();

                return result.Response;
            }
        }

        public async Task<string> GetAgentServiceEndPoint(string serviceName)
        {
            var serviceList = await this.GetAgentServiceList();
            var service = await this.GetAgentService(serviceName);

            return service.Address + ":" + service.Port;
        }

        public async Task<List<AgentService>> GetAgentServiceList()
        {
            var serviceDictionary = await this.GetAgentServiceDictionary();

            List<AgentService> lst = new List<AgentService>();
            foreach (AgentService responseValue in serviceDictionary.Values)
            {
                lst.Add(responseValue);
            }

            return lst;
        }
    }
}