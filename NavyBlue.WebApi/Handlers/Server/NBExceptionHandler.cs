// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NBExceptionHandler.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:24
// *****************************************************************************************************************
// <copyright file="NBExceptionHandler.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers.Server
{
    /// <summary>
    ///     NBExceptionHandler.
    /// </summary>
    public class NBExceptionHandler : ExceptionHandler
    {
        /// <summary>
        ///     When overridden in a derived class, handles the exception synchronously.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ErrorResult
            {
                Exception = context.Exception,
                Request = context.ExceptionContext.Request
            };
        }

        /// <summary>
        ///     Determines whether the exception should be handled.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        /// <returns>true if the exception should be handled; otherwise, false.</returns>
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        #region Nested type: ErrorResult

        /// <summary>
        ///     ErrorResult. This class cannot be inherited.
        /// </summary>
        public sealed class ErrorResult : IHttpActionResult
        {
            /// <summary>
            ///     Gets or sets the exception.
            /// </summary>
            /// <value>The exception.</value>
            public Exception Exception { get; set; }

            /// <summary>
            ///     Gets or sets the request.
            /// </summary>
            /// <value>The request.</value>
            public HttpRequestMessage Request { get; set; }

            /// <summary>
            ///     Executes the asynchronous.
            /// </summary>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response;
                if (this.Exception is NotImplementedException)
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                }
                else
                {
                    response = this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, this.Exception);
                }

                return Task.FromResult(response);
            }
        }

        #endregion Nested type: ErrorResult
    }
}