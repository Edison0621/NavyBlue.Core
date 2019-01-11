using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NavyBlue.AspNetCore.Web.Middlewares.Middleware
{
    public interface INavyBlueMiddleware
    {
        Task Invoke(HttpContext context);
    }
}
