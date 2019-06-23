using AutoMapper;
using BlogAppCore.Application.Infrastructure.AutoMapper;

namespace BlogAppCore.Application.Tests.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TagProfile());
                mc.AddProfile(new CategoryProfile());
                mc.AddProfile(new PostProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}