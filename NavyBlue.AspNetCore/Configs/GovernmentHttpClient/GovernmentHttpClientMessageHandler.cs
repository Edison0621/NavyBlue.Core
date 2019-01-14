// *****************************************************************************************************************
// Project          : NavyBlue
// File             : GovernmentHttpClientMessageHandler.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:11
// *****************************************************************************************************************
// <copyright file="GovernmentHttpClientMessageHandler.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace NavyBlue.NetCore.Lib.Configs.GovernmentHttpClient
{
    /// <summary>
    ///     GovernmentHttpClientMessageHandler.
    /// </summary>
    public class GovernmentHttpClientMessageHandler : DelegatingHandler
    {
        private const string CRYPTO_SERVICE_PROVIDER_ERROR_MESSAGE = "GovernmentHttpClientMessageHandler RSACryptoServiceProvider can not initialize. The AppKeys may be in bad format. AppKeys: {0}";
        private static readonly RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(2048);

        /// <summary>
        ///     Initializes a new instance of the <see cref="GovernmentHttpClientMessageHandler" /> class.
        /// </summary>
        public GovernmentHttpClientMessageHandler()
        {
            try
            {
                cryptoServiceProvider.FromXmlString(App.Host.AppKeys);
            }
            catch (Exception e)
            {
                throw new Exception(CRYPTO_SERVICE_PROVIDER_ERROR_MESSAGE.FormatWith(App.Host.AppKeys), e);
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
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string sign = cryptoServiceProvider.SignData(App.Host.Role.GetBytesOfASCII(), new SHA1CryptoServiceProvider()).ToBase64String();
            string ticket = $"{App.Host.Role},{sign}".GetBytesOfASCII().ToBase64String();
            request.Headers.Authorization = new AuthenticationHeaderValue("JIAUTH", ticket);
            request.Headers.TryAddWithoutValidation("X-NB-CID", App.Host.RoleInstance);
            request.Headers.TryAddWithoutValidation("X-NB-RID", Guid.NewGuid().ToGuidString());
            request.Headers.TryAddWithoutValidation("X-NB-SID", Guid.NewGuid().ToGuidString());
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
            request.Headers.AcceptEncoding.Clear();
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip", 0.8));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate", 0.2));
            return base.SendAsync(request, cancellationToken);
        }
    }
}