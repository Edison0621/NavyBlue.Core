// *****************************************************************************************************************
// Project          : NavyBlue
// File             : HttpConfigurationExtensions.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:02
// *****************************************************************************************************************
// <copyright file="HttpConfigurationExtensions.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Builder;
using NavyBlue.AspNetCore.Web.Handlers;
using NavyBlue.AspNetCore.Web.Handlers.Server;

namespace NavyBlue.Lib.Web
{
    /// <summary>
    ///     HttpConfigurationExtensions.
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        ///     Uses the jinyinmao authorization handler.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="bearerAuthKeys">The bearer keys.</param>
        /// <param name="governmentServerPublicKey">The government server public key.</param>
        /// <returns>HttpConfiguration.</returns>
        public static IApplicationBuilder UseJinyinmaoAuthorizationHandler(this IApplicationBuilder builder, string bearerAuthKeys, string governmentServerPublicKey)
        {
            builder.Use(context =>
            {
                new NavyBlueAuthorizationHandler(bearerAuthKeys, governmentServerPublicKey);
                return context;
            });

            return builder;
        }

        ///// <summary>
        /////     Uses the jinyinmao exception handler.
        ///// </summary>
        ///// <param name="config">The configuration.</param>
        ///// <returns>HttpConfiguration.</returns>
        //public static HttpConfiguration UseJinyinmaoExceptionHandler(this HttpConfiguration config)
        //{
        //    config.Services.Replace(typeof(IExceptionHandler), new JinyinmaoExceptionHandler());
        //    return config;
        //}

        ///// <summary>
        /////     Uses the jinyinmao exception logger.
        ///// </summary>
        ///// <param name="config">The configuration.</param>
        ///// <returns>HttpConfiguration.</returns>
        //public static HttpConfiguration UseJinyinmaoExceptionLogger(this HttpConfiguration config)
        //{
        //    config.Services.Add(typeof(IExceptionLogger), new JinyinmaoExceptionLogger());
        //    return config;
        //}

        /// <summary>
        ///     Uses the jinyinmao json response wapper handler.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>HttpConfiguration.</returns>
        public static IApplicationBuilder UseJinyinmaoJsonResponseWapperHandler(this IApplicationBuilder builder)
        {
            builder.Use(context =>
            {
                new NavyBlueJsonResponseWarpperHandler();
                return context;
            });

            return builder;
        }

        ///// <summary>
        /////     Uses the jinyinmao logger.
        ///// </summary>
        ///// <param name="config">The configuration.</param>
        ///// <returns>HttpConfiguration.</returns>
        //public static HttpConfiguration UseJinyinmaoLogger(this HttpConfiguration config)
        //{
        //    config.Services.Replace(typeof(ITraceWriter), new JinyinmaoTraceWriter());
        //    config.Services.Add(typeof(IExceptionLogger), new JinyinmaoExceptionLogger());
        //    return config;
        //}

        ///// <summary>
        /////     Uses the jinyinmao log handler.
        ///// </summary>
        ///// <param name="config">The configuration.</param>
        ///// <returns>HttpConfiguration.</returns>
        //public static HttpConfiguration UseJinyinmaoLogHandler(this HttpConfiguration config)
        //{
        //    config.MessageHandlers.Add(new JinyinmaoLogHandler());
        //    return config;
        //}

        /// <summary>
        ///     Uses the jinyinmao log handler.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="requestTag">The request tag.</param>
        /// <param name="responseTag">The response tag.</param>
        /// <returns>HttpConfiguration.</returns>
        public static IApplicationBuilder UseJinyinmaoLogHandler(this IApplicationBuilder builder, string requestTag, string responseTag)
        {
            builder.Use(context =>
            {
                new NavyBlueLogHandler(requestTag, responseTag);
                return context;
            });

            return builder;
        }

        /// <summary>
        ///     Uses the jinyinmao trace entry handler.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>HttpConfiguration.</returns>
        public static IApplicationBuilder UseJinyinmaoTraceEntryHandler(this IApplicationBuilder builder)
        {
            builder.Use(context =>
            {
                new NavyBlueTraceEntryHandler();
                return context;
            });

            return builder;
        }

        ///// <summary>
        /////     Uses the jinyinmao trace writer.
        ///// </summary>
        ///// <param name="config">The configuration.</param>
        ///// <returns>HttpConfiguration.</returns>
        //public static HttpConfiguration UseJinyinmaoTraceWriter(this HttpConfiguration config)
        //{
        //    config.Services.Replace(typeof(ITraceWriter), new JinyinmaoTraceWriter());
        //    return config;
        //}

        ///// <summary>
        /////     Maps the HTTP batch route.
        ///// </summary>
        ///// <param name="config">The configuration.</param>
        ///// <returns>HttpConfiguration.</returns>
        //public static HttpConfiguration MapHttpBatchRoute(this HttpConfiguration config)
        //{
        //    config.Routes.MapHttpBatchRoute("WebApiBatch", "$batch", new BatchHandler(GlobalConfiguration.DefaultServer));
        //    return config;
        //}

        ///// <summary>
        /////     Uses the ordered filter.
        ///// </summary>
        ///// <param name="config">The configuration.</param>
        ///// <returns>HttpConfiguration.</returns>
        //public static HttpConfiguration UseOrderedFilter(this HttpConfiguration config)
        //{
        //    config.Services.Replace(typeof(IFilterProvider), new ConfigurationFilterProvider());
        //    config.Services.Add(typeof(IFilterProvider), new OrderedFilterProvider());
        //    return config;
        //}
    }
}