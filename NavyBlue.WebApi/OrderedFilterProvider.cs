// *****************************************************************************************************************
// Project          : NavyBlue
// File             : OrderedFilterProvider.cs
// Created          : 2019-01-09  20:20
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:03
// *****************************************************************************************************************
// <copyright file="OrderedFilterProvider.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace NavyBlue.AspNetCore.Web
{
    /// <summary>
    ///     Class OrderedFilterProvider.
    /// </summary>
    public class OrderedFilterProvider : IFilterProvider
    {
        #region IFilterProvider Members

        public int Order => throw new NotImplementedException();
        //#region IFilterProvider Members TODO qqq

        ///// <summary>
        /////     Gets the filters.
        ///// </summary>
        ///// <param name="configuration">The configuration.</param>
        ///// <param name="actionDescriptor">The action descriptor.</param>
        ///// <returns>IEnumerable&lt;FilterInfo&gt;.</returns>
        //public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        //{
        //    // controller-specific
        //    IEnumerable<FilterInfo> controllerSpecificFilters = OrderFilters(actionDescriptor.ControllerDescriptor.GetFilters(), FilterScope.Controller);

        //    // action-specific
        //    IEnumerable<FilterInfo> actionSpecificFilters = OrderFilters(actionDescriptor.GetFilters(), FilterScope.Action);

        //    return controllerSpecificFilters.Concat(actionSpecificFilters);
        //}

        //#endregion IFilterProvider Members

        ///// <summary>
        /////     Orders the filters.
        ///// </summary>
        ///// <param name="filters">The filters.</param>
        ///// <param name="scope">The scope.</param>
        ///// <returns>IEnumerable&lt;FilterInfo&gt;.</returns>
        //private static IEnumerable<FilterInfo> OrderFilters(IEnumerable<IFilter> filters, FilterScope scope)
        //{
        //    return filters.OfType<IOrderedFilter>()
        //        .OrderBy(filter => filter.Order)
        //        .Select(instance => new FilterInfo(instance, scope));
        //}

        public void OnProvidersExecuted(FilterProviderContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProvidersExecuting(FilterProviderContext context)
        {
            throw new NotImplementedException();
        }

        #endregion IFilterProvider Members
    }
}