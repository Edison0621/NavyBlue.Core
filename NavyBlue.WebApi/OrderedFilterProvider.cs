using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Moe.Lib.Web
{
    /// <summary>
    ///     Class OrderedFilterProvider.
    /// </summary>
    public class OrderedFilterProvider : IFilterProvider
    {
        #region IFilterProvider Members

        /// <summary>
        ///     Gets the filters.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>IEnumerable&lt;FilterInfo&gt;.</returns>
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            // controller-specific
            IEnumerable<FilterInfo> controllerSpecificFilters = OrderFilters(actionDescriptor.ControllerDescriptor.GetFilters(), FilterScope.Controller);

            // action-specific
            IEnumerable<FilterInfo> actionSpecificFilters = OrderFilters(actionDescriptor.GetFilters(), FilterScope.Action);

            return controllerSpecificFilters.Concat(actionSpecificFilters);
        }

        #endregion IFilterProvider Members

        /// <summary>
        ///     Orders the filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <param name="scope">The scope.</param>
        /// <returns>IEnumerable&lt;FilterInfo&gt;.</returns>
        private static IEnumerable<FilterInfo> OrderFilters(IEnumerable<IFilter> filters, FilterScope scope)
        {
            return filters.OfType<IOrderedFilter>()
                .OrderBy(filter => filter.Order)
                .Select(instance => new FilterInfo(instance, scope));
        }
    }
}