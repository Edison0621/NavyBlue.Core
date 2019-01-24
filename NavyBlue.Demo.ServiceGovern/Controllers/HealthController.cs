// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HealthController.cs
// Created          : 2019-01-23  14:33
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-23  14:33
// *****************************************************************************************************************
// <copyright file="HealthController.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Consul;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NavyBlue.Demo.ServiceGovern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        public IActionResult Get()
        {
            return this.Ok(new ServiceEntry
            {
                Node = new Node
                {
                    TaggedAddresses = new Dictionary<string, string> { { "11", "11" } }
                },
                Service = new AgentService
                {
                    Meta = new Dictionary<string, string> { { "11", "11" } }
                },
                Checks = new HealthCheck[] { new HealthCheck() }
            });
        }
    }
}