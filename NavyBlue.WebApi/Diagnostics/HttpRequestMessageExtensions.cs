using System.Net.Http;
using NavyBlue.Lib;
using MoeLib.Diagnostics;
using MoeLib.Web;

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