using System.Linq;
using AutoMapper;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Domain.Entities;

namespace BlogAppCore.Application.Infrastructure.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDetailDto>()
                .ForMember(dest => dest.TotalPosts, opt => opt.MapFrom(src => src.Posts.Count()));

            CreateMap<Category, CategoryListDto>()
                .ForMember(dest => dest.TotalPosts, opt => opt.MapFrom(src => src.Posts.Count()));

            CreateMap<Category, CategoryPreviewDto>();
        }
    }
}