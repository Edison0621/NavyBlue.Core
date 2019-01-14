// *****************************************************************************************************************
// Project          : NavyBlue
// File             : JYMAccessTokenProtector.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  14:58
// *****************************************************************************************************************
// <copyright file="JYMAccessTokenProtector.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using NavyBlue.NetCore.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;

namespace NavyBlue.AspNetCore.Web.Auth
{
    /// <summary>
    ///     NBAccessTokenProtector.
    /// </summary>
    public sealed class NBAccessTokenProtector
    {
        private const string Anonymous = "Anonymous";
        private const string CRYPTO_SERVICE_PROVIDER_ERROR_MESSAGE = "NBAccessTokenProtector RSACryptoServiceProvider can not initialize. The key may be in bad format. Key: {0}";
        private const string Unspecified = "Unspecified";

        /// <summary>
        ///     The RSA crypto service provider
        /// </summary>
        private readonly RSACryptoServiceProvider rsaCryptoServiceProvider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NBAccessTokenProtector" /> class.
        /// </summary>
        /// <param name="key">The cryptographic key.</param>
        public NBAccessTokenProtector(string key)
        {
            try
            {
                this.rsaCryptoServiceProvider = new RSACryptoServiceProvider();
                this.rsaCryptoServiceProvider.FromXmlString(key);
            }
            catch (Exception e)
            {
                throw new ConfigurationErrorsException(CRYPTO_SERVICE_PROVIDER_ERROR_MESSAGE.FormatWith(key), e);
            }
        }

        /// <summary>
        ///     Protects the specified identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns>System.String.</returns>
        public string Protect(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            string name = identity.Name ?? Anonymous;
            Claim claim = identity.FindFirst(ClaimTypes.Expiration);
            long timestamp = claim?.Value?.AsLong() ?? DateTime.UtcNow.UnixTimestamp();
            string scheme = identity.AuthenticationType ?? Unspecified;
            string payload = $"{name},{timestamp},{scheme}";

            return this.rsaCryptoServiceProvider.Encrypt(payload.GetBytesOfASCII(), false).ToBase64String();
        }

        /// <summary>
        ///     Unprotects the specified protected data.
        /// </summary>
        /// <param name="protectedData">The protected data.</param>
        /// <returns>System.Security.Claims.ClaimsIdentity.</returns>
        public ClaimsIdentity Unprotect(string protectedData)
        {
            if (protectedData == null)
            {
                throw new ArgumentNullException(nameof(protectedData));
            }

            List<Claim> claims = new List<Claim>();

            try
            {
                byte[] unprotectedData = this.rsaCryptoServiceProvider.Decrypt(protectedData.ToBase64Bytes(), false);
                string identityData = unprotectedData.ASCII();
                string[] identityDatas = identityData.Split(',');
                long timestamp = identityDatas[1]?.AsLong() ?? 0L;
                if (timestamp < DateTime.UtcNow.UnixTimestamp())
                {
                    claims.Add(new Claim(ClaimTypes.Expired, "True"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Name, identityDatas[0] ?? Anonymous));
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                    return new ClaimsIdentity(claims, identityDatas[2] ?? Unspecified);
                }
            }
            catch (Exception e)
            {
                claims.Add(new Claim(ClaimTypes.AuthorizationDecision, "Error:" + e.Message));
            }

            return new ClaimsIdentity(claims);
        }
    }
}