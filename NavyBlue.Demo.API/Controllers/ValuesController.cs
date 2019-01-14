// *****************************************************************************************************************
// Project          : NavyBlue
// File             : ValuesController.cs
// Created          : 2019-01-10  18:53
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  19:25
// *****************************************************************************************************************
// <copyright file="ValuesController.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Mvc;
using NavyBlue.AspNetCore.Web;
using NavyBlue.Demo.API.Model;
using System.Collections.Generic;

namespace NavyBlue.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(UserInfo userInfo)
        {
            return this.Ok(new { UserName = "edison",Age = 30 });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}