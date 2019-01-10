namespace NavyBlue.AspNetCore.Web.Web.Auth
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
        /// The jym internal authentication
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