// ***********************************************************************
// Project          : MoeLib
// File             : BatchHandler.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-27  1:02 AM
// ***********************************************************************
// <copyright file="BatchHandler.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Batch;
using Newtonsoft.Json.Linq;

namespace Moe.Lib.Web
{
    /// <summary>
    ///     Class BatchHandler.
    /// </summary>
    public class BatchHandler : DefaultHttpBatchHandler
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BatchHandler" /> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public BatchHandler(HttpServer server)
            : base(server)
        {
            this.SupportedContentTypes.Add("application/x-www-form-urlencoded");
            this.SupportedContentTypes.Add("text/json");
            this.SupportedContentTypes.Add("application/json");
            this.ExecutionOrder = BatchExecutionOrder.NonSequential;
        }

        /// <summary>
        ///     create response message as an asynchronous operation.
        /// </summary>
        /// <param name="responses">The responses.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        public override async Task<HttpResponseMessage> CreateResponseMessageAsync(IList<HttpResponseMessage> responses,
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            List<JsonResponseMessage> jsonResponses = new List<JsonResponseMessage>();
            foreach (HttpResponseMessage subResponse in responses)
            {
                JsonResponseMessage jsonResponse = new JsonResponseMessage
                {
                    Code = (int)subResponse.StatusCode
                };
                // only add cookie to the header
                foreach (KeyValuePair<string, IEnumerable<string>> header in subResponse.Headers)
                {
                    jsonResponse.Headers.Add(header.Key, string.Join(",", header.Value));
                }
                if (subResponse.Content != null)
                {
                    jsonResponse.Body = await subResponse.Content.ReadAsStringAsync();
                    foreach (KeyValuePair<string, IEnumerable<string>> header in subResponse.Content.Headers)
                    {
                        jsonResponse.Headers.Add(header.Key, string.Join(",", header.Value));
                    }
                }
                jsonResponses.Add(jsonResponse);
            }

            return request.CreateResponse(HttpStatusCode.OK, jsonResponses);
        }

        /// <summary>
        ///     parse batch requests as an asynchronous operation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IList&lt;HttpRequestMessage&gt;&gt;.</returns>
        public override async Task<IList<HttpRequestMessage>> ParseBatchRequestsAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            JsonRequestMessage[] jsonSubRequests = await this.ParseRequestMessages(request);

            // Only support for get for the moment
            IEnumerable<HttpRequestMessage> subRequests = jsonSubRequests.Where(r => r.Method.ToUpper() == "GET").Select(r =>
            {
                Uri subRequestUri = new Uri(request.RequestUri, "/" + r.RelativeUrl);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod(r.Method), subRequestUri);

                // copy all http headers from request, and do not copy the content headers (do not support POST | PUT)
                httpRequestMessage.CopyHeadersFrom(request);
                return httpRequestMessage;
            });
            return subRequests.ToList();
        }

        /// <summary>
        ///     Gets the json request messages.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;JsonRequestMessage[]&gt;.</returns>
        private static async Task<JsonRequestMessage[]> GetJsonRequestMessages(HttpRequestMessage request)
        {
            return await request.Content.ReadAsAsync<JsonRequestMessage[]>();
        }

        /// <summary>
        ///     Gets the urlencode request messages.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;JsonRequestMessage[]&gt;.</returns>
        private static async Task<JsonRequestMessage[]> GetUrlencodeRequestMessages(HttpRequestMessage request)
        {
            string requestContent = (await request.Content.ReadAsFormDataAsync()).Get("batch");
            return JObject.Parse(requestContent).GetValue("requests").ToObject<JsonRequestMessage[]>();
        }

        /// <summary>
        ///     Parses the request messages.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;JsonRequestMessage[]&gt;.</returns>
        /// <exception cref="System.ArgumentException">Invalid batch requests format;request</exception>
        private async Task<JsonRequestMessage[]> ParseRequestMessages(HttpRequestMessage request)
        {
            string contentType = request.Content.Headers.ContentType.MediaType;
            switch (contentType)
            {
                case "application/x-www-form-urlencoded":
                    return await GetUrlencodeRequestMessages(request);

                case "text/json":
                case "application/json":
                    return await GetJsonRequestMessages(request);
            }

            throw new ArgumentException(@"Invalid batch requests format", nameof(request));
        }
    }
}