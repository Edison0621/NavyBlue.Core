// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IConsulConfigurationExtensions.cs
// Created          : 2019-01-30  10:33
// 
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-30  10:33
// *****************************************************************************************************************
// <copyright file="IConsulConfigurationExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************


using Consul;
using Microsoft.Extensions.Configuration;
using NavyBlue.AspNetCore.Lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NavyBlue.AspNetCore.ConsulConfiguration.Extensions
{
    public static class ConsulConfigurationExtensions
    {
        public static T GetConsulConfiguration<T>(this IConfiguration configuration, string key) where T : IConsulConfiguration
        {
            return configuration[key].FromJson<T>();
        }

        internal static bool HasValue(this KVPair kvPair)
        {
            return kvPair.IsLeafNode() && kvPair.Value != null && kvPair.Value.Any();
        }

        internal static bool HasValue(this QueryResult<KVPair[]> queryResult)
        {
            return queryResult != null
                   && queryResult.StatusCode != HttpStatusCode.NotFound
                   && queryResult.Response != null
                   && queryResult.Response.Any(kvp => kvp.HasValue());
        }

        internal static bool IsLeafNode(this KVPair kvPair)
        {
            return !kvPair.Key.EndsWith("/");
        }

        internal static string ToConfig(this KVPair kvPair)
        {
            using (Stream stream = new MemoryStream(kvPair.Value))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        jsonReader.DateParseHandling = DateParseHandling.None;
                        JObject jsonConfig = JObject.Load(jsonReader);

                        if (jsonConfig.GetDuplicatedString() != string.Empty)
                        {
                            throw new Exception($"Json键有重复,重复值是{jsonConfig.GetDuplicatedString()}，请验证后再次启动");
                        }

                        return jsonConfig.ToString();
                    }
                }
            }
        }

        private static string GetDuplicatedString(this JObject jObject)
        {
            IDictionary<string, string> data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            List<string> keys = jObject.Properties().Select(p => p.Name).ToList();

            var duplicatedList = keys.Where((st, index) => keys.FindIndex(p => p != st) == index).ToList();
            if (duplicatedList.Count <= 0)
            {
                return string.Empty;
            }

            return string.Join(",", duplicatedList);
        }
    }
}