// *****************************************************************************************************************
// Project          : NavyBlue
// File             : JsonResponseWarpperMiddleware.cs
// Created          : 2019-01-14  17:44
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:54
// *****************************************************************************************************************
// <copyright file="JsonResponseWarpperMiddleware.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    public class JsonResponseWarpperMiddleware : INavyBlueMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonResponseWarpperMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        #region INavyBlueMiddleware Members

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

        #endregion INavyBlueMiddleware Members

        private static async Task WappUpContent(HttpContext context)
        {
            //MemoryWrappedHttpResponseStream stream = new MemoryWrappedHttpResponseStream(context.Response.Body);

            //string content = null;
            //if (context.Response != null)
            //{
            //    stream.EnableReadAsync(context.Response);
            //    content = await stream.ReadBodyAsync(context.Response);
            //}

            //if (content.IsNullOrEmpty())
            //{
            //    content = "{}";
            //}

            //var responseBodyStream = new MemoryStream();
            //await context.Response.Body.CopyToAsync(responseBodyStream);
            //string result = responseBodyStream.GetBuffer().Utf8();

            //JObject jObject = JObject.Parse(content);
            //jObject.Remove("ResultCode");
            //jObject.Remove("ResultMsg");
            //jObject.Add("ResultCode", (context.Response.StatusCode == (int)HttpStatusCode.OK ? "00" : "10") + (int)context.Response.StatusCode);
            //jObject.Add("ResultMsg", context.Response.StatusCode == (int)HttpStatusCode.OK ? "OK" : (jObject.GetValue("message", StringComparison.OrdinalIgnoreCase)?.Value<string>() ?? ""));

            ////response.Content = request.CreateResponse(HttpStatusCode.OK, jObject).Content;

            //await context.Response.WriteAsync(jObject.ToJson());
        }
    }
}