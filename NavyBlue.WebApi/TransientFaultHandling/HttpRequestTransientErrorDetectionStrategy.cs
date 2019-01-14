// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HttpRequestTransientErrorDetectionStrategy.cs
// Created          : 2019-01-14  17:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:24
// *****************************************************************************************************************
// <copyright file="HttpRequestTransientErrorDetectionStrategy.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Net;

namespace NavyBlue.Lib.TransientFaultHandling
{
    /// <summary>
    ///     Transient error detection strategy for http communication between clients and servers.
    /// </summary>
    public class HttpRequestTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        private readonly IList<HttpStatusCode> statusCodes =
            new List<HttpStatusCode>
            {
                HttpStatusCode.RequestTimeout,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.GatewayTimeout
            };

        /// <summary>
        ///     Determines whether the specified exception represents a transient failure that can be compensated by a retry.
        /// </summary>
        /// <param name="ex">The exception object to be verified.</param>
        /// <returns>
        ///     <c>true</c> if the specified exception is considered as transient; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTransient(Exception ex)
        {
            WebException we = ex as WebException;
            HttpWebResponse response = we?.Response as HttpWebResponse;
            return response != null && this.statusCodes.Contains(response.StatusCode);
        }
    }
}