namespace NavyBlue.AspNetCore.Web.Configs.GovernmentHttpClient
{
    /// <summary>
    ///     ApplicationConfigurationsFetchRequest.
    /// </summary>
    public class ApplicationConfigurationsFetchRequest
    {
        /// <summary>
        ///     Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }

        /// <summary>
        ///     Gets or sets the role instance.
        /// </summary>
        /// <value>The role instance.</value>
        public string RoleInstance { get; set; }

        /// <summary>
        ///     Gets or sets the source version.
        /// </summary>
        /// <value>The source version.</value>
        public string SourceVersion { get; set; }
    }
}