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
using NavyBlue.NetCore.Lib;
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
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using NavyBlue.AspNetCore.Web;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    /// <summary>
    ///     AuthorizationMiddleware.
    /// </summary>
    public class AuthorizationMiddleware : INavyBlueMiddleware
    {
        private const string CRYPTO_SERVICE_PROVIDER_ERROR_MESSAGE = "NavyBlueAuthorizationHandler CryptoServiceProvider can not initialize. The GovernmentServerPublicKey may be in bad format. GovernmentServerPublicKey: {0}";
        private readonly NBAccessTokenProtector accessTokenProtector;

        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        static AuthorizationMiddleware()
        {
            //TODO UseSwaggerAsApplicationForDev = CloudConfigurationManager.GetSetting("UseSwaggerAsApplicationForDev").AsBoolean(false);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorizationMiddleware" /> class.
        /// </summary>
        /// <param name="bearerAuthKeys">The bearerAuthKeys.</param>
        public AuthorizationMiddleware(string bearerAuthKeys)
        {
            this.accessTokenProtector = new NBAccessTokenProtector(bearerAuthKeys);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorizationMiddleware" /> class.
        /// </summary>
        /// <param name="bearerAuthKeys">The bearerAuthKeys.</param>
        /// <param name="governmentServerPublicKey">The government server public key.</param>
        public AuthorizationMiddleware(string bearerAuthKeys, string governmentServerPublicKey)
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
            get; //TODO { return new ClaimsIdentity(); } //HttpContext.Current.User?.Identity as ClaimsIdentity; }
            set; //HttpContext.Current.User = new ClaimsPrincipal(value);
        }

        private List<string> IPWhitelists
        {
            get
            {
                return new List<string>();
                //return App.Configurations.GetIPWhitelists();
            }
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
        public async Task Invoke(HttpContext context)
        {
            if (HasAuthorizationHeader(context.Request, NBAuthScheme.Bearer) && context.Request.Headers[HeaderNames.Authorization] != StringValues.Empty)
            {
                this.AuthorizeUserViaBearerToken(context.Request);
            }
            else if (this.GovernmentServerPublicKey != null && HasAuthorizationHeader(context.Request, NBAuthScheme.NBInternalAuth))
            {
                this.AuthorizeApplicationViaAuthToken(context.Request);
            }
            else if (UseSwaggerAsApplicationForDev && this.IsFromSwagger(context.Request))
            {
                this.AuthorizeApplicationIfFromSwagger();
            }
            else if (this.IsFromWhitelists(context.Request))
            {
                this.AuthorizeApplicationIfFromWhitelistst(context.Request);
            }
            else if (HttpUtils.IsFromLocalhost(context))
            {
                this.AuthorizeApplicationIfFromLocalhost();
            }

            if (HasAuthorizationHeader(context.Request)
                && context.Request.Headers["X-NB-Authorization"] == StringValues.Empty
                && this.Identity != null
                && this.Identity.IsAuthenticated
                && this.Identity.AuthenticationType == NBAuthScheme.Bearer
                && context.Response.StatusCode == (int)HttpStatusCode.OK)
            {
                await this.GenerateAndSetAccessToken(context);
            }

            await this._next.Invoke(context);
        }

        private static bool HasAuthorizationHeader(HttpRequest request, string scheme = "Bearer")
        {
            return request.Headers["X-NB-Authorization"] != StringValues.Empty && request.Headers["X-NB-Authorization"].Contains(scheme);
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

        private void AuthorizeApplicationIfFromWhitelistst(HttpRequest request)
        {
            string ip = request.Host.Host;
            this.Identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"IP: {ip}"),
                new Claim(ClaimTypes.Role, "Application")
            }, NBAuthScheme.NBInternalAuth);
        }

        private void AuthorizeApplicationViaAuthToken(HttpRequest request)
        {
            string token = request.Headers["X-NB-Authorization"].ToString().ToBase64Bytes().ASCII();
            string[] tokenPiece = token?.Split(',');
            if (tokenPiece?.Length == 5)
            {
                string ticket = tokenPiece.Take(4).Join(",");
                string sign = tokenPiece[4];
                if (this.CryptoServiceProvider.VerifyData(ticket.GetBytesOfASCII(), new SHA1CryptoServiceProvider(), sign.ToBase64Bytes()))
                {
                    if (tokenPiece[3].AsLong(0) > DateTime.UtcNow.UnixTimestamp() && tokenPiece[1] == "")//TODO App.Host.Role)
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

        private void AuthorizeUserViaBearerToken(HttpRequest request)
        {
            this.Identity = this.accessTokenProtector.Unprotect(request.Headers["X-NB-Authorization"]);
        }

        private async Task GenerateAndSetAccessToken(HttpContext context)
        {
            Claim claim = this.Identity.FindFirst(ClaimTypes.Expiration);
            long timestamp = claim?.Value?.AsLong() ?? DateTime.UtcNow.UnixTimestamp();

            if (context.Response.ContentType == "application/json")
            {
                string content = null;
                if (context.Response != null)
                {
                    //content = context.Response.ReadBodyAsStringAsync();
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

                //context.Response.Body = jObject.ToJson().ToStream(); //request.CreateResponse(response.StatusCode, jObject).Content;
            }
            else
            {
                //context.Response.Body = new
                //{
                //    access_token = this.accessTokenProtector.Protect(this.Identity),
                //    expiration = timestamp
                //}.ToJson().ToStream();
            }
        }

        private bool IsFromSwagger(HttpRequest request)
        {
            if (request.Headers[HeaderNames.Referer] != StringValues.Empty)
            {
                return request.Headers[HeaderNames.Referer].ToString().Contains("swagger", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        private bool IsFromWhitelists(HttpRequest request)
        {
            return this.IPWhitelists != null && this.IPWhitelists.Contains(request.Host.Host);
        }
    }
}