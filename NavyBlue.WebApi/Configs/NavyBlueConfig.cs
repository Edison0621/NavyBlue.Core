using System.Collections.Generic;

namespace NavyBlue.AspNetCore.Web.Configs
{
    internal class NavyBlueConfig : IConfig
    {
        #region IConfig Members

        /// <summary>
        ///     Gets the ip whitelists.
        /// </summary>
        /// <value>The ip whitelists.</value>
        public List<string> IPWhitelists { get; set; }

        /// <summary>
        ///     Gets the resources.
        /// </summary>
        /// <value>The resources.</value>
        public Dictionary<string, string> Resources { get; set; }

        #endregion IConfig Members
    }
}