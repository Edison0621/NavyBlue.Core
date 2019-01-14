using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using NavyBlue.NetCore.Lib;
using Newtonsoft.Json.Linq;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    public class JsonResponseWarpperMiddleware : INavyBlueMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonResponseWarpperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private static async Task WappUpContent(HttpContext context)
        {
            MemoryWrappedHttpResponseStream stream = new MemoryWrappedHttpResponseStream(context.Response.Body);

            string content = null;
            if (context.Response != null)
            {
                stream.EnableReadAsync(context.Response);
                content = await stream.ReadBodyAsync(context.Response);
            }

            if (content.IsNullOrEmpty())
            {
                content = "{}";
            }

            var responseBodyStream = new MemoryStream();
            await context.Response.Body.CopyToAsync(responseBodyStream);
            string result = responseBodyStream.GetBuffer().Utf8();


            JObject jObject = JObject.Parse(content);
            jObject.Remove("ResultCode");
            jObject.Remove("ResultMsg");
            jObject.Add("ResultCode", (context.Response.StatusCode == (int)HttpStatusCode.OK ? "00" : "10") + (int)context.Response.StatusCode);
            jObject.Add("ResultMsg", context.Response.StatusCode == (int)HttpStatusCode.OK ? "OK" : (jObject.GetValue("message", StringComparison.OrdinalIgnoreCase)?.Value<string>() ?? ""));

            //response.Content = request.CreateResponse(HttpStatusCode.OK, jObject).Content;

            await context.Response.WriteAsync(jObject.ToJson());
        }

        public async Task Invoke(HttpContext context)
        {
            await this._next.Invoke(context);

            if (context.Request.Headers[HeaderNames.Accept].ToString() == "application/json" 
                && context.Response != null 
                && context.Response.Headers[HeaderNames.ContentType].ToString().Contains("application/json"))
            {
                await WappUpContent(context);
            }
        }
    }
}
