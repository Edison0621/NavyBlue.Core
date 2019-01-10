// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NavyBlueAuthorizationHandler.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:01
// *****************************************************************************************************************
// <copyright file="NavyBlueAuthorizationHandler.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Http;
using NavyBlue.AspNetCore.Web.Auth;
using NavyBlue.Lib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Handlers.Server
{
    /// <summary>
    ///     NavyBlueAuthorizationHandler.
    /// </summary>
    public class NavyBlueAuthorizationHandler : DelegatingHandler
    {
        private const string CRYPTO_SERVICE_PROVIDER_ERROR_MESSAGE = "NavyBlueAuthorizationHandler CryptoServiceProvider can not initialize. The GovernmentServerPublicKey may be in bad format. GovernmentServerPublicKey: {0}";
        private readonly NBAccessTokenProtector accessTokenProtector;

        static NavyBlueAuthorizationHandler()
        {
            UseSwaggerAsApplicationForDev = CloudConfigurationManager.GetSetting("UseSwaggerAsApplicationForDev").AsBoolean(false);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavyBlueAuthorizationHandler" /> class.
        /// </summary>
        /// <param name="bearerAuthKeys">The bearerAuthKeys.</param>
        public NavyBlueAuthorizationHandler(string bearerAuthKeys)
        {
            this.accessTokenProtector = new NBAccessTokenProtector(bearerAuthKeys);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavyBlueAuthorizationHandler" /> class.
        /// </summary>
        /// <param name="bearerAuthKeys">The bearerAuthKeys.</param>
        /// <param name="governmentServerPublicKey">The government server public key.</param>
        public NavyBlueAuthorizationHandler(string bearerAuthKeys, string governmentServerPublicKey)
        {
            this.accessTokenProtector = new NBAccessTokenProtector(bearerAuthKeys);
            this.GovernmentServerPublicKey = governmentServerPublicKey;
        }

        /// <summary>
        ///     Gets a value indicating whether [use swagger as application for dev].
        /// </summary>
        /// <value><c>true</c> if [use swagger as application for dev]; otherwise, <c>false</c>.</value>
        public static bool UseSwaggerAsApplicationForDev { get; }

        /// <summary>
        ///     Gets or sets the government server public key.
        /// </summary>
        /// <value>The government server public key.</value>
        public string GovernmentServerPublicKey { get; set; }

        private RSACryptoServiceProvider CryptoServiceProvider
        {
            get
            {
                if (this.GovernmentServerPublicKey.IsNullOrEmpty())
                {
                    return null;
                }

                try
                {
                    RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048);
                    provider.FromXmlString(this.GovernmentServerPublicKey);
                    return provider;
                }
                catch (Exception e)
                {
                    throw new ConfigurationErrorsException(CRYPTO_SERVICE_PROVIDER_ERROR_MESSAGE.FormatWith(this.GovernmentServerPublicKey), e);
                }
            }
        }

        private ClaimsIdentity Identity
        {
            get { return HttpContext.Current.User?.Identity as ClaimsIdentity; }
            set { HttpContext.Current.User = new ClaimsPrincipal(value); }
        }

        private List<string> IPWhitelists
        {
            get { return App.Configurations.GetIPWhitelists(); }
        }

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
            FillAuthorizationWithCustomeHeader(request);

            if (HasAuthorizationHeader(request, NBAuthScheme.Bearer) && request.Headers.Authorization?.Parameter != null)
            {
                this.AuthorizeUserViaBearerToken(request);
            }
            else if (this.GovernmentServerPublicKey != null && HasAuthorizationHeader(request, NBAuthScheme.NBInternalAuth))
            {
                this.AuthorizeApplicationViaAuthToken(request);
            }
            else if (UseSwaggerAsApplicationForDev && this.IsFromSwagger(request))
            {
                this.AuthorizeApplicationIfFromSwagger();
            }
            else if (this.IsFromWhitelists(request))
            {
                this.AuthorizeApplicationIfFromWhitelistst(request);
            }
            else if (this.IsFromLocalhost(request))
            {
                this.AuthorizeApplicationIfFromLocalhost();
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (HasAuthorizationHeader(request, NBAuthScheme.Bearer) && request.Headers.Authorization?.Parameter == null
                                                                     && this.Identity != null && this.Identity.IsAuthenticated && this.Identity.AuthenticationType == NBAuthScheme.Bearer && response.StatusCode == HttpStatusCode.OK)
            {
                await this.GenerateAndSetAccessToken(request, response);
            }

            return response;
        }

        private static void FillAuthorizationWithCustomeHeader(HttpRequestMessage request)
        {
            try
            {
                if (!HasAuthorizationHeader(request) && request.Headers.Contains("X-NB-Authorization"))
                {
                    string headerValue = request.GetHeader("X-NB-Authorization");
                    if (headerValue.IsNotNullOrEmpty())
                    {
                        request.Headers.Authorization = AuthenticationHeaderValue.Parse(headerValue);
                    }
                }
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private static bool HasAuthorizationHeader(HttpRequestMessage request, string scheme)
        {
            return request.Headers.Authorization?.Scheme != null &&
                   string.Equals(request.Headers.Authorization.Scheme, scheme, StringComparison.OrdinalIgnoreCase);
        }

        private static bool HasAuthorizationHeader(HttpRequestMessage request)
        {
            return request.Headers.Authorization?.Scheme != null;
        }

        private void AuthorizeApplicationIfFromLocalhost()
        {
            this.Identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Localhost"),
                new Claim(ClaimTypes.Role, "Application")
            }, NBAuthScheme.NBInternalAuth);
        }

        private void AuthorizeApplicationIfFromSwagger()
        {
            this.Identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Swagger"),
                new Claim(ClaimTypes.Role, "Application")
            }, NBAuthScheme.NBInternalAuth);
        }

        private void AuthorizeApplicationIfFromWhitelistst(HttpRequestMessage request)
        {
            string ip = request.GetUserHostAddress();
            this.Identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"IP: {ip}"),
                new Claim(ClaimTypes.Role, "Application")
            }, NBAuthScheme.NBInternalAuth);
        }

        private void AuthorizeApplicationViaAuthToken(HttpRequestMessage request)
        {
            string token = request.Headers.Authorization?.Parameter.ToBase64Bytes().ASCII();
            string[] tokenPiece = token?.Split(',');
            if (tokenPiece?.Length == 5)
            {
                string ticket = tokenPiece.Take(4).Join(",");
                string sign = tokenPiece[4];
                if (this.CryptoServiceProvider.VerifyData(ticket.GetBytesOfASCII(), new SHA1CryptoServiceProvider(), sign.ToBase64Bytes()))
                {
                    if (tokenPiece[3].AsLong(0) > DateTime.UtcNow.UnixTimestamp() && tokenPiece[1] == App.Host.Role)
                    {
                        this.Identity = new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, tokenPiece[0]),
                            new Claim(ClaimTypes.Role, "Application")
                        }, NBAuthScheme.NBInternalAuth);
                    }
                }
            }
        }

        private void AuthorizeUserViaBearerToken(HttpRequestMessage request)
        {
            this.Identity = this.accessTokenProtector.Unprotect(request.Headers.Authorization.Parameter);
        }

        private async Task GenerateAndSetAccessToken(HttpRequestMessage request, HttpResponseMessage response)
        {
            Claim claim = this.Identity.FindFirst(ClaimTypes.Expiration);
            long timestamp = claim?.Value?.AsLong() ?? DateTime.UtcNow.UnixTimestamp();

            if (response.Content?.Headers.ContentType.MediaType == "application/json")
            {
                string content = null;
                if (response.Content != null)
                {
                    content = await response.Content.ReadAsStringAsync();
                }

                if (content.IsNullOrEmpty())
                {
                    content = "{}";
                }

                JObject jObject = JObject.Parse(content);
                jObject.Remove("access_token");
                jObject.Remove("expiration");
                jObject.Add("access_token", this.accessTokenProtector.Protect(this.Identity));
                jObject.Add("expiration", timestamp);

                response.Content = new StringContent(jObject.ToJson()); //request.CreateResponse(response.StatusCode, jObject).Content;
                response.StatusCode = response.StatusCode;
            }
            else
            {
                //response.Content = request.CreateResponse(HttpStatusCode.OK, new
                //{
                //    access_token = this.accessTokenProtector.Protect(this.Identity),
                //    expiration = timestamp
                //}).Content;

                response.Content = new StringContent(new
                {
                    access_token = this.accessTokenProtector.Protect(this.Identity),
                    expiration = timestamp
                }.ToJson()); //request.CreateResponse(response.StatusCode, jObject).Content;
                response.StatusCode = response.StatusCode;
            }
        }

        private bool IsFromLocalhost(HttpRequestMessage request)
        {
            return HttpUtils.IsFromLocalhost(request);
        }

        private bool IsFromSwagger(HttpRequestMessage request)
        {
            if (request.Headers.Referrer != null)
            {
                return request.Headers.Referrer.AbsoluteUri.Contains("swagger", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private bool IsFromWhitelists(HttpRequestMessage request)
        {
            return this.IPWhitelists != null && this.IPWhitelists.Contains(request.GetUserHostAddress());
        }
    }
}