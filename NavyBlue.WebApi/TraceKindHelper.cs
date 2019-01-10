// *****************************************************************************************************************
// Project          : NavyBlue
// File             : TraceKindHelper.cs
// Created          : 2019-01-10  10:17
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:03
// *****************************************************************************************************************
// <copyright file="TraceKindHelper.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

namespace NavyBlue.WebApi
{
    internal static class TraceKindHelper
    {
        public static bool IsDefined(TraceKind traceKind)
        {
            if (traceKind != TraceKind.Trace && traceKind != TraceKind.Begin)
                return traceKind == TraceKind.End;
            return true;
        }

        public static void Validate(TraceKind value, string parameterValue)
        {
            if (!IsDefined(value))
                throw Error.InvalidEnumArgument(parameterValue, (int)value, typeof(TraceKind));
        }
    }
}