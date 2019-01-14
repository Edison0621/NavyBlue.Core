// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NavyBlueLogHandler.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:02
// *****************************************************************************************************************
// <copyright file="NavyBlueLogHandler.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using MoeLib.Diagnostics;
using NavyBlue.AspNetCore.Web.Diagnostics;
using NavyBlue.NetCore.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers
{
    /// <summary>
    ///     JinyinmaoLogHandler.
    /// </summary>
    public class NavyBlueLogHandler : DelegatingHandler
    {
        /// <summary>
        ///     The logger
        /// </summary>
        private static readonly Lazy<IWebLogger> logger = new Lazy<IWebLogger>(() => InitApplicationLogger());

        private static readonly string[] mediaTypes = { "application/json", "application/xml", "application/jsonp" };

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavyBlueLogHandler" /> class.
        /// </summary>
        public NavyBlueLogHandler(string requestTag, string responseTag)
        {
            this.RequestTag = requestTag;
            this.ResponseTag = responseTag;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavyBlueLogHandler" /> class.
        /// </summary>
        public NavyBlueLogHandler()
        {
            this.RequestTag = "ASP.NET HTTP Request";
            this.ResponseTag = "ASP.NET HTTP Response";
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private IWebLogger Logger
        {
            get { return logger.Value; }
        }

        private string RequestTag { get; set; }

        private string ResponseTag { get; set; }

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
            TraceEntry traceEntry = request?.GetTraceEntry();

            string requestIdString = traceEntry?.RequestId ?? Guid.NewGuid().ToGuidString();

            this.Logger.Info($"Request Begin: {requestIdString}", request, this.RequestTag, 0UL, string.Empty, traceEntry);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Dictionary<string, object> payload = new Dictionary<string, object>
            {
                { "ResponseStatusCode", response.StatusCode }
            };

            if (response.Content != null && mediaTypes.Any(t => string.Equals(t, response.Content.Headers.ContentType.MediaType, StringComparison.OrdinalIgnoreCase)))
            {
                payload.Add("ResponseContent", (await response.Content.ReadAsStringAsync()).GetFirst(30000));
            }

            this.Logger.Info($"Request End: {requestIdString}", request, this.ResponseTag, 0UL, string.Empty, traceEntry, null, payload);

            return response;
        }

        private static IWebLogger InitApplicationLogger()
        {
            return App.LogManager.CreateWebLogger();
        }
    }
}