using System.Collections.Generic;

namespace NavyBlue.AspNetCore.Web.Configs
{
    /// <summary>
    ///     Interface IConfig
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets the ip whitelists.
        /// </summary>
        /// <value>The ip whitelists.</value>
        List<string> IPWhitelists { get; }

        /// <summary>
        ///     Gets the resources.
        /// </summary>
        /// <value>The resources.</value>
        Dictionary<string, string> Resources { get; }
    }

    /// <summary>
    ///     Interface IConfig
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public interface IConfig<out TConfig> : IConfig where TConfig : class
    {
        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        TConfig Config { get; }
    }
}