// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ServiceRegisterEntity.cs
// Created          : 2019-01-24  16:00
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-24  16:28
// *****************************************************************************************************************
// <copyright file="ServiceRegisterEntity.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;

namespace NavyBlue.ServiceGovern
{
    public class ServiceRegisterInfo
    {
        public ServiceRegisterInfo()
        {
            HealthCheck = new AgentCheckRegistration();
        }

        public string ConsulServer { get; set; }

        public bool IsHttps { get; set; }

        public bool IsIPV6 { get; set; }
        public string ServiceIP { get; set; }

        public string ServiceName { get; set; }

        public int ServicePort { get; set; }

        public AgentCheckRegistration HealthCheck { get; set; }
    }
}