// *****************************************************************************************************************
// Project          : NavyBlue
// File             : UserAuthorizeAttribute.cs
// Created          : 2019-01-09  20:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:01
// *****************************************************************************************************************
// <copyright file="UserAuthorizeAttribute.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Authorization;

namespace NavyBlue.AspNetCore.Web.Filters
{
    /// <summary>
    ///     UserAuthorizeAttribute.
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAuthorizeAttribute" /> class.
        /// </summary>
        public UserAuthorizeAttribute()
        {
            this.Roles = "User";
        }
    }
}