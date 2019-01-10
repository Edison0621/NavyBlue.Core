using System;

namespace NavyBlue.AspNetCore.Web.Configs
{
    /// <summary>
    ///     Interface IConfigProvider
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        ///     Gets the type of the configuration.
        /// </summary>
        /// <returns>Type.</returns>
        Type GetConfigType();

        /// <summary>
        ///     Gets the configurations string.
        /// </summary>
        /// <returns>System.String.</returns>
        SourceConfig GetSourceConfig();
    }
}