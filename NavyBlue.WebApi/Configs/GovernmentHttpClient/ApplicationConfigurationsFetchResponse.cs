using System.Collections.Generic;

namespace NavyBlue.AspNetCore.Web.Configs.GovernmentHttpClient
{
    /// <summary>
    ///     ApplicationConfigurationsFetchResponse.
    /// </summary>
    public class ApplicationConfigurationsFetchResponse
    {
        /// <summary>
        ///     Gets or sets the configurations.
        /// </summary>
        /// <value>The configurations.</value>
        public string Configurations { get; set; }

        /// <summary>
        ///     Gets or sets the configuration version.
        /// </summary>
        /// <value>The configuration version.</value>
        public string ConfigurationVersion { get; set; }

        /// <summary>
        ///     Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public Dictionary<string, KeyValuePair<string, string>> Permissions { get; set; }
    }
}