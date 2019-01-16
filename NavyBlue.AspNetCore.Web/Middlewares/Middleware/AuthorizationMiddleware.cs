// *****************************************************************************************************************
// Project          : NavyBlue
// File             : AuthorizationMiddleware.cs
// Created          : 2019-01-14  17:44
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  15:58
// *****************************************************************************************************************
// <copyright file="AuthorizationMiddleware.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using NavyBlue.AspNetCore.Web.Extensions;
using NavyBlue.AspNetCore.Lib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    /// <summary>
    ///     AuthorizationMiddleware.
    /// </summary>
    public class AuthorizationMiddleware : INavyBlueMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NBAccessTokenProtector accessTokenProtector;
        private readonly HttpContext httpContext;

        public AuthorizationMiddleware(RequestDelegate next, HttpContext httpContext, string bearerAuthKeys, string governmentServerPublicKey)
        {
            this._next = next;
            this.accessTokenProtector = new NBAccessTokenProtector(bearerAuthKeys);
            this.GovernmentServerPublicKey = governmentServerPublicKey;
            this.httpContext = httpContext;
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

        private RSA CryptoServiceProvider
        {
            get
            {
                if (this.GovernmentServerPublicKey.IsNullOrEmpty())
                {
                    return null;
                }

                try
                {
                    RSA rsa = RSA.Create();
                    rsa.FromXmlString(this.GovernmentServerPublicKey);

                    return rsa;
                }
                catch (Exception e)
                {
                    throw new ConfigurationErrorsException("Bad format key with {0}".FormatWith(this.GovernmentServerPublicKey), e);
                }
            }
        }

        private ClaimsIdentity Identity
        {
            get { return this.httpContext.User?.Identity as ClaimsIdentity; }
            set { this.httpContext.User = new ClaimsPrincipal(value); }
        }

        private List<string> IPWhitelists
        {
            get { return App.Configurations.GetIPWhitelists(); }
        }

        #region INavyBlueMiddleware Members

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
            if (HasAuthorizationHeader(context.Request, NBAuthScheme.Bearer) && context.Request.Headers["X-NB-Authorization"] != StringValues.Empty)
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
            else if (HttpRequestExtensions.IsFromLocalhost(context))
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

        #endregion INavyBlueMiddleware Members

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

                if (this.CryptoServiceProvider.VerifyData(ticket.GetBytesOfASCII(), sign.ToBase64Bytes(), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1))
                {
                    if (tokenPiece[3].AsLong(0) > DateTime.UtcNow.UnixTimestamp() && tokenPiece[1] == "") //TODO App.Host.Role)
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

        /// <summary>
        /// Generates the and set access token.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
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

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(jObject.ToJson());

                //context.Response.Body = jObject.ToJson().ToStream(); //request.CreateResponse(response.StatusCode, jObject).Content;
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