using Microsoft.AspNetCore.Builder;

namespace AlgoTecture.Middleware.CustomExceptionMiddleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}