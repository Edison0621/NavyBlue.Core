// *****************************************************************************************************************
// Project          : NavyBlue
// File             : BaseApiController.cs
// Created          : 2019-01-09  20:20
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:02
// *****************************************************************************************************************
// <copyright file="BaseApiController.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     MoeApiController.
    /// </summary>
    public abstract class BaseApiController : Controller
    {
        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ITraceWriter Logger = new DiagnosticsTraceWriter();
    }
}