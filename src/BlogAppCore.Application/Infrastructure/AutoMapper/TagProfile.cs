using System.Linq;
using AutoMapper;
using BlogAppCore.Application.Tags.Models;
using BlogAppCore.Domain.Entities;

namespace BlogAppCore.Application.Infrastructure.AutoMapper
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDetailDto>()
                .ForMember(dest => dest.TotalPosts, opt => opt.MapFrom(src => src.PostTags.Count()));

            CreateMap<Tag, TagPreviewDto>();
        }
    }
}