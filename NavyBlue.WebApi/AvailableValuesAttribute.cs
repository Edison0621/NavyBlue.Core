using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Moe.Lib.Web
{
    /// <summary>
    ///     Determines whether the specified value of the object is valid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class AvailableValuesAttribute : ValidationAttribute
    {
        private readonly string[] availableValues;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AvailableValuesAttribute" /> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public AvailableValuesAttribute(params object[] values)
            : base(@"The {0} value is not available.")
        {
            this.availableValues = values.Select(v => v.ToString()).ToArray();
        }

        /// <summary>
        ///     Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <returns>
        ///     An instance of the formatted error message.
        /// </returns>
        /// <param name="name">The name to include in the formatted message.</param>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, name);
        }

        /// <summary>
        ///     Determines whether the specified value of the object is valid.
        /// </summary>
        /// <returns>
        ///     true if the specified value is valid; otherwise, false.
        /// </returns>
        /// <param name="value">The value of the object to validate. </param>
        public override bool IsValid(object value)
        {
            return value != null && this.availableValues.Contains(value.ToString());
        }
    }
}