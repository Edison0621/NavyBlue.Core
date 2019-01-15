using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

        public static void UseMyExceptionHandler(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder => {

                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        //记录日志
                        var logger = loggerFactory.CreateLogger("NavyBlue.Api.Extensions.ExceptionHandlingExtensions");
                        logger.LogDebug(500, ex.Error, ex.Error.Message);
                    }
                    await context.Response.WriteAsync(ex?.Error?.Message ?? "an error occure");
                });
            });
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
        }
    }
}
