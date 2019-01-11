using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using NavyBlue.Lib;
using Microsoft.Extensions.Primitives;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    public class TraceEntryMiddleware : INavyBlueMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public TraceEntryMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<TraceEntryMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Handling API key for: " + context.Request.Path);

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

            await _next.Invoke(context);

            _logger.LogInformation("Finished tracing.");
        }

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

        private List<string> IPWhitelists
        {
            get
            {
                return new List<string>();
                //return App.Configurations.GetIPWhitelists();
            }
        }
    }
}
