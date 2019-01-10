using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers.Client
{
    /// <summary>
    ///     JinyinmaoAcceptHandler.
    /// </summary>
    public class NavyBlueAcceptHandler : DelegatingHandler
    {
        /// <summary>
        ///     Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was null.</exception>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.5));
            return await base.SendAsync(request, cancellationToken);
        }
    }
}