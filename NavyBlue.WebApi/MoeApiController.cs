// ***********************************************************************
// Project          : MoeLib
// File             : MoeApiController.cs
// Created          : 2015-11-23  2:24 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-26  11:09 PM
// ***********************************************************************
// <copyright file="MoeApiController.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace MoeLib.Web
{
    /// <summary>
    ///     MoeApiController.
    /// </summary>
    public abstract class MoeApiController : Controller
    {
        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ITraceWriter Logger
        {
            get { return this.Configuration.Services.GetTraceWriter(); }
        }
    }
}