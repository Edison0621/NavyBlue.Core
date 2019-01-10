// *****************************************************************************************************************
// Project          : NavyBlue
// File             : TraceKind.cs
// Created          : 2019-01-10  10:14
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  15:03
// *****************************************************************************************************************
// <copyright file="TraceKind.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

namespace NavyBlue.WebApi
{
    /// <summary>Specifies the kind of tracing operation.</summary>
    public enum TraceKind
    {
        /// <summary>Single trace, not part of a Begin/End trace pair.</summary>
        Trace,

        /// <summary>Trace marking the beginning of some operation.</summary>
        Begin,

        /// <summary>Trace marking the end of some operation.</summary>
        End
    }
}