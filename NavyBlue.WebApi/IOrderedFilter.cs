// *****************************************************************************************************************
// Project          : NavyBlue
// File             : IOrderedFilter.cs
// Created          : 2019-01-09  20:20
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:03
// *****************************************************************************************************************
// <copyright file="IOrderedFilter.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     Class OrderedActionFilterAttribute.
    /// </summary>
    public class OrderedActionFilterAttribute : ActionFilterAttribute, IOrderedFilter
    {
        #region IOrderedFilter Members

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        #endregion IOrderedFilter Members
    }

    /// <summary>
    ///     Class OrderedAuthorizationFilterAttribute.
    /// </summary>
    public class OrderedAuthorizationFilterAttribute : Attribute, IAuthorizationFilter, IOrderedFilter
    {
        #region IAuthorizationFilter Members

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }

        #endregion IAuthorizationFilter Members

        #region IOrderedFilter Members

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        #endregion IOrderedFilter Members
    }

    /// <summary>
    ///     Class OrderedExceptionFilterAttribute.
    /// </summary>
    public class OrderedExceptionFilterAttribute : ExceptionFilterAttribute, IOrderedFilter
    {
        #region IOrderedFilter Members

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        #endregion IOrderedFilter Members
    }
}