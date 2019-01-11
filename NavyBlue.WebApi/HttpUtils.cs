// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HttpUtils.cs
// Created          : 2019-01-09  20:20
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:03
// *****************************************************************************************************************
// <copyright file="HttpUtils.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using NavyBlue.Lib;
using ReflectionMagic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     HttpUtils.
    /// </summary>
    public static class HttpUtils
    {
        /// <summary>
        ///     Clones an <see cref="HttpWebRequest" /> in order to send it again.
        /// </summary>
        /// <param name="message">The message to set headers on.</param>
        /// <param name="request">The request with headers to clone.</param>
        public static void CopyHeadersFrom(this HttpRequestMessage message, HttpRequest request)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            foreach (string headerName in request.Headers.Keys)
            {
                string[] headerValues = request.Headers[headerName];
                if (!message.Headers.TryAddWithoutValidation(headerName, headerValues))
                {
                    message.Content.Headers.TryAddWithoutValidation(headerName, headerValues);
                }
            }
        }

        /// <summary>
        ///     Clones an <see cref="HttpWebRequest" /> in order to send it again.
        /// </summary>
        /// <param name="message">The message to set headers on.</param>
        /// <param name="request">The request with headers to clone.</param>
        public static void CopyHeadersFrom(this HttpRequestMessage message, HttpRequestMessage request)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers.AsEnumerable())
            {
                if (!message.Headers.TryAddWithoutValidation(header.Key, header.Value))
                {
                    message.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        ///     Dumps the specified request include headers.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="includeHeaders">if set to <c>true</c> [include headers].</param>
        /// <returns>System.String.</returns>
        public static string Dump(this HttpRequest httpRequest, bool includeHeaders = true)
        {
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                TextWriter writer = new StreamWriter(memoryStream);

                writer.Write(httpRequest.Method);
                writer.Write(httpRequest.HttpContext.Request.GetDisplayUrl());

                // headers

                if (includeHeaders)
                {
                    if (httpRequest.AsDynamic()._wr != null)
                    {
                        // real request -- add protocol
                        writer.Write(" " + httpRequest.AsDynamic()._wr.GetHttpVersion() + "\r\n");

                        // headers
                        writer.Write(httpRequest.AsDynamic().CombineAllHeaders(true));
                    }
                    else
                    {
                        // manufactured request
                        writer.Write("\r\n");
                    }
                }

                writer.Write("\r\n");
                writer.Flush();

                // entity body

                dynamic httpInputStream = httpRequest.AsDynamic().InputStream;
                httpInputStream.WriteTo(memoryStream);

                StreamReader reader = new StreamReader(memoryStream);
                return reader.ReadToEnd();
            }
            finally
            {
                memoryStream.Close();
            }
        }

        /// <summary>
        ///     Dumps the specified request include headers.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="includeHeaders">if set to <c>true</c> [include headers].</param>
        /// <returns>System.String.</returns>
        public static string Dump(this HttpRequestMessage httpRequest, bool includeHeaders = true)
        {
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                TextWriter writer = new StreamWriter(memoryStream);

                writer.Write(httpRequest.Method + "\r\n");
                writer.Write(httpRequest.RequestUri.AbsoluteUri + "\r\n");
                writer.Write(httpRequest.Version + "\r\n");

                // headers

                if (includeHeaders)
                {
                    foreach (KeyValuePair<string, IEnumerable<string>> header in httpRequest.Headers)
                    {
                        writer.Write(header.Key + ": " + header.Value.Join(","));
                    }
                }

                writer.Write("\r\n");
                writer.Flush();

                // entity body

                Task<string> contentTask = httpRequest.Content.ReadAsStringAsync();
                contentTask.Wait();
                writer.Write(contentTask.Result);

                StreamReader reader = new StreamReader(memoryStream);
                return reader.ReadToEnd();
            }
            finally
            {
                memoryStream.Close();
            }
        }

        /// <summary>
        ///     Returns an individual cookie from the cookies collection.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <returns>The cookie value. Return null if the cookie does not exist.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        /// <exception cref="System.ArgumentNullException">If the cookieName is null, throw the ArgumentNullException.</exception>
        public static string GetCookie(HttpRequestMessage request, string cookieName)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (cookieName == null)
            {
                throw new ArgumentNullException(nameof(cookieName));
            }

            return request.GetCookie(cookieName);
        }

        /// <summary>
        ///     Returns an individual HTTP Header value that joins all the header value with ' '.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <param name="key">The key of the header.</param>
        /// <returns>The HTTP Header value that joins all the header value with ' '. Return null if the header does not exist.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        /// <exception cref="System.ArgumentNullException">If the key is null, throw the ArgumentNullException.</exception>
        public static string GetHeader(HttpRequestMessage request, string key)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IEnumerable<string> keys;
            if (!request.Headers.TryGetValues(key, out keys))
                return null;

            return keys.Join(" ");
        }

        /// <summary>
        ///     Returns an individual querystring value.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <param name="key">The key.</param>
        /// <returns>The querystring value. Return null if the querystring does not exist.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        /// <exception cref="System.ArgumentNullException">If the key is null, throw the ArgumentNullException.</exception>
        public static string GetQueryStringValue(this HttpRequest request, string key)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // IEnumerable<KeyValuePair<string,string>> - right!
            IQueryCollection queryStrings = request.Query;
            if (queryStrings == null)
                return null;

            KeyValuePair<string, StringValues> match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, StringComparison.OrdinalIgnoreCase) == 0);
            return string.IsNullOrEmpty(match.Value) ? StringValues.Empty : match.Value;
        }

        /// <summary>
        ///     Returns a dictionary of QueryStrings that's easier to work with
        ///     than GetQueryNameValuePairs KevValuePairs collection.
        ///     If you need to pull a few single values use GetQueryString instead.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <returns>The QueryStrings dictionary.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        public static Dictionary<string, string> GetQueryStrings(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.GetQueryStrings()
                .ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Returns the user agent string value.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <returns>The user agent string value.</returns>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        public static string GetUserAgent(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.UserAgent.ToString();
        }

        /// <summary>
        ///     Returns the user agent string value.
        /// </summary>
        /// <param name="httpContext">The instance of <see cref="HttpContext" />.</param>
        /// <returns>The user agent string value.</returns>
        public static string GetUserAgent(HttpContext httpContext)
        {
            return httpContext.Request.Headers[HeaderNames.UserAgent].ToString();
        }

        /// <summary>
        ///     Returns the user host(ip) string value.
        /// </summary>
        /// <param name="request">The instance of <see cref="HttpRequestMessage" />.</param>
        /// <exception cref="System.ArgumentNullException">If the request is null, throw the ArgumentNullException.</exception>
        public static string GetUserHostAddress(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.Host;
        }

        /// <summary>
        ///     Returns the user host(ip) string value.
        /// </summary>
        /// <param name="httpContext">The instance of <see cref="HttpContext" />.</param>
        public static string GetUserHostAddress(HttpContext httpContext)
        {
            return httpContext == null ? "" : httpContext.Request.Headers[HeaderNames.Host].ToString();
        }

        /// <summary>
        ///     Determines whether the specified HTTP httpContext is from dev.
        /// </summary>
        /// <param name="httpContext">The HTTP httpContext.</param>
        /// <param name="ipStartWith">The ip start with.</param>
        /// <returns><c>true</c> if the specified HTTP httpContext is dev; otherwise, <c>false</c>.</returns>
        public static bool IsFrom(HttpContext httpContext, string ipStartWith)
        {
            string ip = GetUserHostAddress(httpContext);
            return !string.IsNullOrEmpty(ip) && ip.StartsWith(ipStartWith, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Determines whether the specified request is from ios.
        /// </summary>
        /// <param name="httpContext">The HTTP httpContext.</param>
        /// <returns><c>true</c> if the specified request is ios; otherwise, <c>false</c>.</returns>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static bool IsFromIos(HttpContext httpContext)
        {
            string userAgent = GetUserAgent(httpContext);
            return userAgent != null && (userAgent.ToUpperInvariant().Contains("IPHONE") || userAgent.ToUpperInvariant().Contains("IPAD") || userAgent.ToUpperInvariant().Contains("IPOD"));
        }

        /// <summary>
        ///     Determines whether the specified HTTP httpContext is from localhost.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns><c>true</c> if the specified HTTP httpContext is localhost; otherwise, <c>false</c>.</returns>
        public static bool IsFromLocalhost(HttpContext httpContext)
        {
            return httpContext.Connection.LocalIpAddress.MapToIPv6().IsIPv6SiteLocal;
        }

        /// <summary>
        ///     Determines whether the specified HTTP httpContext is from localhost.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns><c>true</c> if the specified HTTP httpContext is localhost; otherwise, <c>false</c>.</returns>
        public static bool IsFromLocalhost(HttpRequestMessage response)
        {
            return response.Headers.Host == "::1";
        }

        /// <summary>
        ///     Determines whether the specified request is from mobile device.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns><c>true</c> if the specified request is from mobile device; otherwise, <c>false</c>.</returns>
        public static bool IsFromMobileDevice(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return false;
            }

            string userAgent = GetUserAgent(httpContext);
            if (userAgent.IsNullOrEmpty())
            {
                return false;
            }

            userAgent = userAgent.ToUpperInvariant();

            return userAgent.Contains("IPHONE") || userAgent.Contains("IOS") || userAgent.Contains("IPAD")
                   || userAgent.Contains("ANDROID");
        }
    }
}