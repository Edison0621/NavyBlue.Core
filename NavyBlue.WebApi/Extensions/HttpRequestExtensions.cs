// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HttpRequestExtensions.cs
// Created          : 2019-01-14  17:59
// 
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:59
// *****************************************************************************************************************
// <copyright file="HttpRequestExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************


using Microsoft.AspNetCore.Http;

namespace NavyBlue.AspNetCore.Web.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetHeader(this HttpRequest request,string headerName)
        {
            return request.Headers[headerName].ToString();
        }
    }
}