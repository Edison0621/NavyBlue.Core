// ***********************************************************************
// Project          : MoeLib
// File             : Host.cs
// Created          : 2015-11-20  5:55 PM
//
// Last Modified By : Siqi Lu(lu.siqi@outlook.com)
// Last Modified On : 2015-11-25  12:35 PM
// ***********************************************************************
// <copyright file="Host.cs" company="Shanghai Yuyi Mdt InfoTech Ltd.">
//     Copyright ©  2012-2015 Shanghai Yuyi Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// ***********************************************************************

using System;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     Host.
    /// </summary>
    public class Host
    {
        internal Lazy<string> appKeys;

        internal Lazy<Guid> deploymentId;

        internal Lazy<string> environment;

        internal Lazy<string> role;

        internal Lazy<string> roleInstance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Host" /> class.
        /// </summary>
        internal Host()
        {
        }

        /// <summary>
        /// Gets the application keys.
        /// </summary>
        /// <value>The application keys.</value>
        public string AppKeys
        {
            get { return this.appKeys.Value; }
        }

        /// <summary>
        ///     Gets or sets the deployment identifier.
        /// </summary>
        /// <value>The deployment identifier.</value>
        public Guid DeploymentId
        {
            get { return this.deploymentId.Value; }
        }

        /// <summary>
        ///     Gets or sets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public string Environment
        {
            get { return this.environment.Value; }
        }

        /// <summary>
        ///     Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role
        {
            get { return this.role.Value; }
        }

        /// <summary>
        ///     Gets or sets the role instance.
        /// </summary>
        /// <value>The role instance.</value>
        public string RoleInstance
        {
            get { return this.roleInstance.Value; }
        }

        /// <summary>
        ///     Determines whether [is in azure cloud].
        /// </summary>
        /// <returns><c>true</c> if [is in azure cloud]; otherwise, <c>false</c>.</returns>
        public bool IsInAzureCloud()
        {
            return false;
            //return RoleEnvironment.IsAvailable;
        }
    }
}