// *****************************************************************************************************************
// Project          : NavyBlue
// File             : GovernmentServerConfigProvider.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:56
// *****************************************************************************************************************
// <copyright file="GovernmentServerConfigProvider.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.NetCore.Lib.Configs.GovernmentHttpClient;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using NavyBlue.AspNetCore;

namespace NavyBlue.NetCore.Lib.Configs
{
    /// <summary>
    ///     GovernmentServerConfigProvider.
    /// </summary>
    /// <typeparam name="TConfig">The type of the t configuration.</typeparam>
    public class GovernmentServerConfigProvider<TConfig> : IConfigProvider where TConfig : class, IConfig
    {
        private static readonly Lazy<HttpClient> httpClient = new Lazy<HttpClient>(() => InitHttpClient());

        private HttpClient HttpClient
        {
            get { return httpClient.Value; }
        }

        #region IConfigProvider Members

        /// <summary>
        ///     Gets the type of the configuration.
        /// </summary>
        /// <returns>Type.</returns>
        public Type GetConfigType()
        {
            return typeof(TConfig);
        }

        /// <summary>
        ///     Gets the configurations string.
        /// </summary>
        /// <returns>System.String.</returns>
        public SourceConfig GetSourceConfig()
        {
            try
            {
                ApplicationConfigurationsFetchRequest request = new ApplicationConfigurationsFetchRequest
                {
                    Role = App.Host.Role,
                    RoleInstance = App.Host.RoleInstance,
                    SourceVersion = App.Configurations.GetConfigurationVersion()
                };

                return Task.Run(async () =>
                {
                    HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("/api/Configurations", request);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return content.FromJson<SourceConfig>();
                    }

                    throw new HttpRequestException($"Can not get \"Configurations\" from government server {this.HttpClient.BaseAddress}, {response.StatusCode} {response.ReasonPhrase}");
                }).Result;
            }
            catch (Exception e)
            {
                throw new Exception("Missing config of \"Configurations\"", e);
            }
        }

        #endregion IConfigProvider Members

        private static Uri GetGovernmentBaseUri()
        {
            return new Uri("http://localhost:6753/");
        }

        private static HttpClient InitHttpClient()
        {
            HttpClient client = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            })
            {
                BaseAddress = GetGovernmentBaseUri(),
                Timeout = 1.Minutes()
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(NBAuthScheme.NBInternalAuth, RSATicket);
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-NB-CID", App.Host.RoleInstance);
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-NB-RID", Guid.NewGuid().ToGuidString());
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-NB-SID", Guid.NewGuid().ToGuidString());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
            client.DefaultRequestHeaders.AcceptEncoding.Clear();
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip", 0.8));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate", 0.2));

            return client;
        }

        private static string RSATicket
        {
            get
            {
                RSA rsa = RSA.Create();
                rsa.FromRSAXmlString(App.Host.AppKeys);

                string sign = rsa.SignData(App.Host.Role.GetBytesOfASCII(), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1).ToBase64String();
                string ticket = $"{App.Host.Role},{sign}".GetBytesOfASCII().ToBase64String();

                return ticket;
            }
        }
    }
}