using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NavyBlue.NetCore.Lib;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    public class ExceptionHandlingMiddleware:INavyBlueMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var ex = context.Features.Get<IExceptionHandlerFeature>();
            if (ex != null)
            {
                //记录日志
                App.LogManager.CreateLogger().Error(ex.Error.Message,"NavyBlue.ExceptionHandlingExtensions",exception:ex.Error);
            }

            await context.Response.WriteAsync(ex?.Error?.Message ?? "an error occure");

            await this.next.Invoke(context);
        }
    }
}
