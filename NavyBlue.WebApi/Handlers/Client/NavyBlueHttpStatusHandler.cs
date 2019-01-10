using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers.Client
{
    /// <summary>
    ///     JinyinmaoHttpStatusHandler.
    /// </summary>
    public class NavyBlueHttpStatusHandler : DelegatingHandler
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
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new HttpRequestException($"404: Can not find the service. {request.RequestUri.AbsoluteUri}");
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        throw new HttpRequestException($"{(int)response.StatusCode}: Can not access the service. {request.RequestUri.AbsoluteUri}");
                }

                if ((int)response.StatusCode < 500)
                {
                    throw new HttpRequestException($"{(int)response.StatusCode}: Bad request to the service. {request.RequestUri.AbsoluteUri}");
                }

                throw new HttpRequestException($"{(int)response.StatusCode}: The service is unavailable. {request.RequestUri.AbsoluteUri}");
            }

            return response;
        }
    }
}