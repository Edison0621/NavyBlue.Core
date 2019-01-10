using System.Collections.Generic;

namespace NavyBlue.AspNetCore.Web.Configs
{
    /// <summary>
    ///     SourceConfig.
    /// </summary>
    public class SourceConfig
    {
        /// <summary>
        ///     Gets the configurations.
        /// </summary>
        /// <value>The configurations.</value>
        public string Configurations { get; set; }

        /// <summary>
        ///     Gets the configuration version.
        /// </summary>
        /// <value>The configuration version.</value>
        public string ConfigurationVersion { get; set; }

        /// <summary>
        ///     Gets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public Dictionary<string, KeyValuePair<string, string>> Permissions { get; set; }
    }
}