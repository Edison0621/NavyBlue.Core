// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HttpRequestMessageExtensions.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  14:59
// *****************************************************************************************************************
// <copyright file="HttpRequestMessageExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using MoeLib.Diagnostics;
using NavyBlue.Lib;
using System.Net.Http;

namespace NavyBlue.AspNetCore.Web.Diagnostics
{
    /// <summary>
    ///     HttpRequestMessageExtensions.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        ///     Gets the trace entry.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>MoeLib.Diagnostics.TraceEntry.</returns>
        public static TraceEntry GetTraceEntry(this HttpRequestMessage request)
        {
            return request.To(r => new TraceEntry
            {
                ClientId = request?.GetHeader("X-NB-CID"),
                DeviceId = request?.GetHeader("X-NB-DID"),
                RequestId = request?.GetHeader("X-NB-RID"),
                SessionId = request?.GetHeader("X-NB-SID"),
                SourceIP = request?.GetHeader("X-NB-IP") ?? request?.GetUserHostAddress(),
                SourceUserAgent = request?.GetHeader("X-NB-UA") ?? request?.GetUserAgent(),
                UserId = request?.GetHeader("X-NB-UID")
            });
        }
    }
}