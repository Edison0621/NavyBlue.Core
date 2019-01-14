// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NBTraceEntryHandler.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:24
// *****************************************************************************************************************
// <copyright file="NBTraceEntryHandler.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.AspNetCore.Web.Diagnostics;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers.Client
{
    /// <summary>
    ///     NBTraceEntryHandler.
    /// </summary>
    public class NBTraceEntryHandler : DelegatingHandler
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NBTraceEntryHandler" /> class.
        /// </summary>
        /// <param name="traceEntry">The trace entry.</param>
        public NBTraceEntryHandler(TraceEntry traceEntry)
        {
            this.TraceEntry = traceEntry;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NBTraceEntryHandler" /> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public NBTraceEntryHandler(HttpRequestMessage request)
        {
            this.TraceEntry = request.GetTraceEntry();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NBTraceEntryHandler" /> class.
        /// </summary>
        /// <param name="traceEntry">The trace entry.</param>
        /// <param name="userId">The user identifier.</param>
        public NBTraceEntryHandler(TraceEntry traceEntry, string userId)
        {
            this.TraceEntry = traceEntry;
            if (userId.IsNotNullOrEmpty())
            {
                this.TraceEntry.UserId = userId;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NBTraceEntryHandler" /> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        public NBTraceEntryHandler(HttpRequestMessage request, string userId)
        {
            this.TraceEntry = request.GetTraceEntry();
            if (userId.IsNotNullOrEmpty())
            {
                this.TraceEntry.UserId = userId;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NBTraceEntryHandler" /> class.
        /// </summary>
        public NBTraceEntryHandler()
        {
            this.TraceEntry = null;
        }

        private TraceEntry TraceEntry { get; set; }

        /// <summary>
        ///     Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (this.TraceEntry == null)
            {
                request.Headers.TryAddWithoutValidation("X-NB-CID", App.Host.RoleInstance);
                request.Headers.TryAddWithoutValidation("X-NB-RID", Guid.NewGuid().ToGuidString());
                request.Headers.TryAddWithoutValidation("X-NB-SID", Guid.NewGuid().ToGuidString());
            }
            else
            {
                request.Headers.TryAddWithoutValidation("X-NB-CID", this.TraceEntry.ClientId + "," + App.Host.RoleInstance);
                request.Headers.TryAddWithoutValidation("X-NB-RID", this.TraceEntry.RequestId ?? Guid.NewGuid().ToGuidString());
                request.Headers.TryAddWithoutValidation("X-NB-SID", this.TraceEntry.SessionId ?? Guid.NewGuid().ToGuidString());
                if (this.TraceEntry.SourceIP.IsNullOrEmpty())
                {
                    request.Headers.TryAddWithoutValidation("X-NB-IP", this.TraceEntry.SourceIP);
                }

                if (this.TraceEntry.SourceUserAgent.IsNullOrEmpty())
                {
                    request.Headers.TryAddWithoutValidation("X-NB-UA", this.TraceEntry.SourceUserAgent);
                }

                if (this.TraceEntry.UserId.IsNullOrEmpty())
                {
                    request.Headers.TryAddWithoutValidation("X-NB-UID", this.TraceEntry.UserId);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}