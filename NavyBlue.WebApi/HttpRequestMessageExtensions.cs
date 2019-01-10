using System.Collections.Generic;
using System.Net.Http;
using Moe.Lib.Web;

namespace MoeLib.Web
{
    /// <summary>
    ///     Extends the HttpRequestMessage.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        ///     Returns an individual cookie from the cookies collection.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <returns>The cookie value. Return null if the cookie does not exist.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        /// <exception cref="System.ArgumentNullException">If the cookieName is null, throw the ArgumentNullException.</exception>
        public static string GetCookie(this HttpRequestMessage request, string cookieName)
        {
            return HttpUtils.GetCookie(request, cookieName);
        }

        /// <summary>
        ///     Returns an individual HTTP Header value that joins all the header value with ' '.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <param name="key">The key of the header.</param>
        /// <returns>The HTTP Header value that joins all the header value with ' '. Return null if the header does not exist.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        /// <exception cref="System.ArgumentNullException">If the key is null, throw the ArgumentNullException.</exception>
        public static string GetHeader(this HttpRequestMessage request, string key)
        {
            return HttpUtils.GetHeader(request, key);
        }

        /// <summary>
        ///     Returns an individual querystring value.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <param name="key">The key.</param>
        /// <returns>The querystring value. Return null if the querystring does not exist.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        /// <exception cref="System.ArgumentNullException">If the key is null, throw the ArgumentNullException.</exception>
        public static string GetQueryString(this HttpRequestMessage request, string key)
        {
            return HttpUtils.GetQueryString(request, key);
        }

        /// <summary>
        ///     Returns a dictionary of QueryStrings that's easier to work with
        ///     than GetQueryNameValuePairs KevValuePairs collection.
        ///     If you need to pull a few single values use GetQueryString instead.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <returns>The QueryStrings dictionary.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        public static Dictionary<string, string> GetQueryStrings(this HttpRequestMessage request)
        {
            return HttpUtils.GetQueryStrings(request);
        }

        /// <summary>
        ///     Returns the user agent string value.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        public static string GetUserAgent(this HttpRequestMessage request)
        {
            return HttpUtils.GetUserAgent(request);
        }

        /// <summary>
        ///     Returns the user host(ip) string value.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        public static string GetUserHostAddress(this HttpRequestMessage request)
        {
            return HttpUtils.GetUserHostAddress(request);
        }
    }
}