using System.Reflection;
using AutoMapper;
using BlogAppCore.Application.Infrastructure.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(new Assembly[] { typeof(PostProfile).GetTypeInfo().Assembly });

            return services;
        }
    }
}