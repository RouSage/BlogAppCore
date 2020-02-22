using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using BlogAppCore.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;

                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<BlogAppCoreDbContext>()
                .AddDefaultTokenProviders();

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
