using BlogAppCore.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

namespace BlogAppCore.Extensions
{
    public static class ConfigureExceptionMiddleware
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}