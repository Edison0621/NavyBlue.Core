// *****************************************************************************************************************
// Project          : NavyBlue
// File             : NBAuthScheme.cs
// Created          : 2019-01-14  17:08
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-14  17:13
// *****************************************************************************************************************
// <copyright file="NBAuthScheme.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

namespace NavyBlue.NetCore.Lib
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
        ///     The internal authentication
        /// </summary>
        public static readonly string NBInternalAuth = "JIAUTH";

        /// <summary>
        ///     Quick
        /// </summary>
        public static readonly string NBQuick = "Quick";

        /// <summary>
        ///     WeChat
        /// </summary>
        public static readonly string NBWeChat = "WeChat";
    }
}