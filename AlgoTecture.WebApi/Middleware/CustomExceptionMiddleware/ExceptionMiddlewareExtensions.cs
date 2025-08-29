using Microsoft.AspNetCore.Builder;

namespace Algotecture.WebApi.Middleware.CustomExceptionMiddleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}