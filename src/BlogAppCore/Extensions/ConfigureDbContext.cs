using BlogAppCore.Application.Interfaces;
using BlogAppCore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<IBlogAppCoreDbContext, BlogAppCoreDbContext>(options =>
                    options.UseNpgsql(connectionString))
                .BuildServiceProvider();

            return services;
        }
    }
}