using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NavyBlue.Lib;
using NavyBlue.Lib.Jinyinmao;
using MoeLib.Diagnostics;
using NavyBlue.AspNetCore.Web.Web.Auth;
using NavyBlue.AspNetCore.Web.Web.Diagnostics;
using NavyBlue.AspNetCore.Web.Web.Handlers;
using NavyBlue.AspNetCore.Web.Web.Handlers.Client;

namespace NavyBlue.AspNetCore.Web.Web
{
    /// <summary>
    ///     NBHttpClient.
    /// </summary>
    public static class NBInternalHttpClientFactory
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="T:System.Net.Http.HttpClient" />.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="handlers">The list of HTTP handler that delegates the processing of HTTP response messages to another handler.</param>
        /// <returns>A new instance of the <see cref="T:System.Net.Http.HttpClient" />.</returns>
        public static HttpClient Create(string serviceName, TraceEntry traceEntry, params DelegatingHandler[] handlers)
        {
            List<DelegatingHandler> delegatingHandlers = new List<DelegatingHandler>
            {
                // new JinyinmaoServicePermissionHandler(serviceName),
                new JinyinmaoTraceEntryHandler(traceEntry),
                new JinyinmaoHttpStatusHandler(),
                new JinyinmaoLogHandler("HTTP Client Request", "HTTP Client Response"),
                new JinyinmaoRetryHandler()
            };
            delegatingHandlers.AddRange(handlers);

            HttpClient client = HttpClientFactory.Create(new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }, delegatingHandlers.ToArray());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.5));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.1));
            client.DefaultRequestHeaders.AcceptEncoding.Clear();
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip", 1.0));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate", 0.5));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("*", 0.1));
            client.Timeout = 1.Minutes();

            KeyValuePair<string, string>? permission = App.Configurations.GetPermission(serviceName);
            if (permission.HasValue)
            {
                client.BaseAddress = new Uri(permission.Value.Key);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(NBAuthScheme.NBInternalAuth, permission.Value.Value);
            }
            else
            {
                client.BaseAddress = new Uri("http://service.jinyinmao.com.cn/");
            }

            return client;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:System.Net.Http.HttpClient" />.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="handlers">The list of HTTP handler that delegates the processing of HTTP response messages to another handler.</param>
        /// <returns>A new instance of the <see cref="T:System.Net.Http.HttpClient" />.</returns>
        public static HttpClient Create(string serviceName, TraceEntry traceEntry, string userId, params DelegatingHandler[] handlers)
        {
            if (userId.IsNotNullOrEmpty())
            {
                traceEntry.UserId = userId;
            }

            return Create(serviceName, traceEntry, handlers);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:System.Net.Http.HttpClient" />.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="request">The request.</param>
        /// <param name="handlers">The list of HTTP handler that delegates the processing of HTTP response messages to another handler.</param>
        /// <returns>A new instance of the <see cref="T:System.Net.Http.HttpClient" />.</returns>
        public static HttpClient Create(string serviceName, HttpRequestMessage request, params DelegatingHandler[] handlers)
        {
            return Create(serviceName, request, "", handlers);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:System.Net.Http.HttpClient" />.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="handlers">The list of HTTP handler that delegates the processing of HTTP response messages to another handler.</param>
        /// <returns>A new instance of the <see cref="T:System.Net.Http.HttpClient" />.</returns>
        public static HttpClient Create(string serviceName, HttpRequestMessage request, string userId, params DelegatingHandler[] handlers)
        {
            return Create(serviceName, request.GetTraceEntry(), userId, handlers);
        }
    }
}