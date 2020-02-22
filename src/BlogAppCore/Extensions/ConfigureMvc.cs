using BlogAppCore.Application.Posts.Commands.Create;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePostCommandValidator>());
            services.AddRazorPages();

            return services;
        }

        public static IApplicationBuilder ConfigureRouting(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            return app;
        }
    }
}
