// *****************************************************************************************************************
// Project          : NavyBlue
// File             : TraceEntryMiddleware.cs
// Created          : 2019-01-14  17:44
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:54
// *****************************************************************************************************************
// <copyright file="TraceEntryMiddleware.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using NavyBlue.NetCore.Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    public class TraceEntryMiddleware : INavyBlueMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceEntryMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        private List<string> IPWhitelists
        {
            get
            {
                return new List<string>();
                //return App.Configurations.GetIPWhitelists();
            }
        }

        #region INavyBlueMiddleware Members

        public async Task Invoke(HttpContext context)
        {
            App.LogManager.CreateLogger().Info("Handling API key for: " + context.Request.Path);

            if (!context.Items.ContainsKey("X-NB-CID"))
            {
                string clientId = context.Request.Host.Host;
                if (this.IsFromSwagger(context.Request))
                {
                    clientId = "Swagger_" + clientId;
                }
                else if (this.IsFromWhitelists(context.Request))
                {
                    clientId = "Whitelist_" + clientId;
                }
                else if (this.IsFromLocalhost(context))
                {
                    clientId = "Localhost_" + clientId;
                }

                context.Items.Add("X-NB-CID", clientId);
            }

            if (!context.Request.Headers.ContainsKey("X-NB-DID"))
            {
                context.Items.Add("X-NB-DID", "0");
            }

            if (!context.Request.Headers.ContainsKey("X-NB-RID"))
            {
                context.Items.Add("X-NB-RID", Guid.NewGuid().ToGuidString());
            }

            if (!context.Request.Headers.ContainsKey("X-NB-SID"))
            {
                context.Items.Add("X-NB-SID", Guid.NewGuid().ToGuidString());
            }

            if (!context.Request.Headers.ContainsKey("X-NB-IP"))
            {
                context.Items.Add("X-NB-IP", context.Request.Host.Host);
            }

            if (!context.Request.Headers.ContainsKey("X-NB-UA"))
            {
                context.Items.Add("X-NB-UA", context.Request.Host.Port);
            }

            if (!context.Request.Headers.ContainsKey("X-NB-UID"))
            {
                context.Items.Add("X-NB-UID", "Anonymous");
            }

            await this._next.Invoke(context);

            App.LogManager.CreateLogger().Info("Finished tracing.");
        }

        #endregion INavyBlueMiddleware Members

        private bool IsFromLocalhost(HttpContext context)
        {
            return context.Request.HttpContext.Connection.LocalIpAddress.MapToIPv6().ToString() == "::1";
        }

        private bool IsFromSwagger(HttpRequest request)
        {
            if (request.Headers[HeaderNames.Referer] != StringValues.Empty)
            {
                return request.Headers[HeaderNames.Referer].ToString().Contains("swagger", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private bool IsFromWhitelists(HttpRequest request)
        {
            return this.IPWhitelists != null && this.IPWhitelists.Contains(request.Headers[HeaderNames.Host]);
        }
    }
}