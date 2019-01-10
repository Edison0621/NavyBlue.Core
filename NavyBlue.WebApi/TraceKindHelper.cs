using System;
using System.Collections.Generic;
using System.Text;

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
      if (!TraceKindHelper.IsDefined(value))
        throw Error.InvalidEnumArgument(parameterValue, (int) value, typeof (TraceKind));
    }
  }
}
