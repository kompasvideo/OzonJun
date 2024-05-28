using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Route256_2_Demo.Middleware
{
    public static class MyMiddlewareExtension
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app, ILogger logger)
        {
            app.Use(async (context, next) =>
            {
                logger.LogInformation("MyMiddlewareExtension Before");
                next.Invoke();
                logger.LogInformation("MyMiddlewareExtension After");
            });
            return app;
        }
    }
}