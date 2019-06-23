using System.Linq;
using AutoMapper;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Domain.Entities;

namespace BlogAppCore.Application.Infrastructure.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostPreviewDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(t => t.Tag)));

            CreateMap<Post, PostDetailDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(t => t.Tag)));
        }
    }
}