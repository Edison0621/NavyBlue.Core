using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace NavyBlue.AspNetCore.Web.Web.Handlers.Server
{
    /// <summary>
    ///     JinyinmaoExceptionHandler.
    /// </summary>
    public class JinyinmaoExceptionHandler : ExceptionHandler
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

            #region IHttpActionResult Members

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

            #endregion IHttpActionResult Members
        }

        #endregion Nested type: ErrorResult
    }
}