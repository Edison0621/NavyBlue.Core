// *****************************************************************************************************************
// Project          : NavyBlue
// File             : JYMAuthScheme.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  14:58
// *****************************************************************************************************************
// <copyright file="JYMAuthScheme.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

namespace NavyBlue.AspNetCore.Web.Auth
{
    /// <summary>
    ///     NBAuthScheme.
    /// </summary>
    public static class NBAuthScheme
    {
        /// <summary>
        ///     Basic
        /// </summary>
        public static readonly string Basic = "Basic";

        /// <summary>
        ///     Bearer
        /// </summary>
        public static readonly string Bearer = "Bearer";

        /// <summary>
        ///     The jym internal authentication
        /// </summary>
        public static readonly string NBInternalAuth = "JIAUTH";

        /// <summary>
        ///     NBQuick
        /// </summary>
        public static readonly string NBQuick = "NBQuick";

        /// <summary>
        ///     NBWeChat
        /// </summary>
        public static readonly string NBWeChat = "NBWeChat";
    }
}