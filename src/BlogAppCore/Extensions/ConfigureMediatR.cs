using System.Reflection;
using BlogAppCore.Application.Infrastructure;
using BlogAppCore.Application.Posts.Queries.GetPostDetail;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAppCore.Extensions
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetPostDetailQueryHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            return services;
        }
    }
}