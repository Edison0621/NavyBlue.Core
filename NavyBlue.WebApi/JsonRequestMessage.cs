namespace Moe.Lib.Web
{
    /// <summary>
    ///     Class JsonRequestMessage.
    /// </summary>
    public class JsonRequestMessage
    {
        /// <summary>
        ///     Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; set; }

        /// <summary>
        ///     Gets or sets the relative URL.
        /// </summary>
        /// <value>The relative URL.</value>
        public string RelativeUrl { get; set; }
    }
}