using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using NavyBlue.Lib;
using NavyBlue.AspNetCore.Web.Auth;

namespace NavyBlue.AspNetCore.Web.Handlers.Client
{
    /// <summary>
    ///     JinyinmaoServicePermissionHandler.
    /// </summary>
    public class NavyBlueServicePermissionHandler : DelegatingHandler
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NavyBlueServicePermissionHandler" /> class.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        public NavyBlueServicePermissionHandler(string serviceName)
        {
            this.ServiceName = serviceName;
        }

        private string ServiceName { get; set; }

        /// <summary>
        ///     Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (!ignorePermission)
            //{
            //    KeyValuePair<string, string>? permission = App.Configurations.GetPermission(this.ServiceName);
            //    if (!permission.HasValue)
            //    {
            //        this.HandleUnauthorizedRequest();
            //    }
            //    else
            //    {
            //        if (request.RequestUri.Host == "mock.nb.com.cn")
            //        {
            //            request.RequestUri = new Uri(new Uri(permission.Value.Key), request.RequestUri.PathAndQuery);
            //        }
            //        request.Headers.Authorization = new AuthenticationHeaderValue(NBAuthScheme.NBInternalAuth, permission.Value.Value);
            //    }
            //}

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                this.HandleUnauthorizedRequest();
            }

            return response;
        }

        private void HandleUnauthorizedRequest()
        {
            throw new HttpRequestException($"Do not have permission to access the service {this.ServiceName}");
        }
    }
}