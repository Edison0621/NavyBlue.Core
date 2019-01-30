// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IServiceDiscovery.cs
// Created          : 2019-01-24  17:02
// 
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-24  17:02
// *****************************************************************************************************************
// <copyright file="IServiceDiscovery.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************


using Consul;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyBlue.ServiceGovern
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// Gets the agent service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns></returns>
        Task<AgentService> GetAgentService(string serviceName);

        /// <summary>
        /// Gets the agent service dictionary.
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, AgentService>> GetAgentServiceDictionary();

        /// <summary>
        /// Gets the agent service end point.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns></returns>
        Task<string> GetAgentServiceEndPoint(string serviceName);

        /// <summary>
        /// Gets the agent service list.
        /// </summary>
        /// <returns></returns>
        Task<List<AgentService>> GetAgentServiceList();
    }
}