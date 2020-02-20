using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using BlogAppCore.Persistence;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BlogAppCoreDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, BlogAppCoreDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
