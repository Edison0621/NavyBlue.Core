// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NavyBlueTraceEntryHandler.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:01
// *****************************************************************************************************************
// <copyright file="NavyBlueTraceEntryHandler.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.Lib;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers.Server
{
    /// <summary>
    ///     JinyinmaoTraceEntryHandler.
    /// </summary>
    public class NavyBlueTraceEntryHandler : DelegatingHandler
    {
        private List<string> IPWhitelists
        {
            get { return App.Configurations.GetIPWhitelists(); }
        }

        /// <summary>
        ///     Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-NB-CID"))
            {
                string clientId = request.GetUserHostAddress();
                if (this.IsFromSwagger(request))
                {
                    clientId = "Swagger_" + clientId;
                }
                else if (this.IsFromWhitelists(request))
                {
                    clientId = "Whitelist_" + clientId;
                }
                else if (this.IsFromLocalhost(request))
                {
                    clientId = "Localhost_" + clientId;
                }

                request.Headers.TryAddWithoutValidation("X-NB-CID", clientId);
            }

            if (!request.Headers.Contains("X-NB-DID"))
            {
                request.Headers.TryAddWithoutValidation("X-NB-DID", "0");
            }

            if (!request.Headers.Contains("X-NB-RID"))
            {
                request.Headers.TryAddWithoutValidation("X-NB-RID", Guid.NewGuid().ToGuidString());
            }

            if (!request.Headers.Contains("X-NB-SID"))
            {
                request.Headers.TryAddWithoutValidation("X-NB-SID", Guid.NewGuid().ToGuidString());
            }

            if (!request.Headers.Contains("X-NB-IP"))
            {
                request.Headers.TryAddWithoutValidation("X-NB-IP", request.GetUserHostAddress());
            }

            if (!request.Headers.Contains("X-NB-UA"))
            {
                request.Headers.TryAddWithoutValidation("X-NB-UA", request.GetUserAgent());
            }

            if (!request.Headers.Contains("X-NB-UID"))
            {
                request.Headers.TryAddWithoutValidation("X-NB-UID", "Anonymous");
            }

            return base.SendAsync(request, cancellationToken);
        }

        private bool IsFromLocalhost(HttpRequestMessage request)
        {
            return false;
            //TODO return request.Headers.Connection.();
        }

        private bool IsFromSwagger(HttpRequestMessage request)
        {
            if (request.Headers.Referrer != null)
            {
                return request.Headers.Referrer.AbsoluteUri.Contains("swagger", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private bool IsFromWhitelists(HttpRequestMessage request)
        {
            return this.IPWhitelists != null && this.IPWhitelists.Contains(request.GetUserHostAddress());
        }
    }
}