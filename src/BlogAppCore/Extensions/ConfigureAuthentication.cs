using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using BlogAppCore.Persistence;
using Microsoft.AspNetCore.Builder;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BlogAppCoreDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, BlogAppCoreDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
        public static IApplicationBuilder ConfigureAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            return app;
        }
    }

}
