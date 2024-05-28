using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Route256_2_Demo.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<MyMiddleware> logger)
        {
            logger.LogInformation("MyMiddleware Before");
            await _next.Invoke(context);
            logger.LogInformation("MyMiddleware After");
        }
    }
}