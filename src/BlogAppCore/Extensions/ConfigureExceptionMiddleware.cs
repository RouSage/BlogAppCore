using BlogAppCore.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IApplicationBuilder ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}