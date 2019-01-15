// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HttpClientExtensions.cs
// Created          : 2019-01-10  17:44
// 
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  17:44
// *****************************************************************************************************************
// <copyright file="HttpClientExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************


using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NavyBlue.NetCore.Lib;

namespace NavyBlue.AspNetCore
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            StringContent content = new StringContent(data.ToJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PostAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();

            return dataAsString.FromJson<T>();
        }
    }
}