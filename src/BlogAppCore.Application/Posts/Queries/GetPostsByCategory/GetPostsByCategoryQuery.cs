using System.Collections.Generic;
using BlogAppCore.Application.Posts.Models;
using MediatR;

namespace BlogAppCore.Application.Posts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQuery : IRequest<List<PostPreviewDto>>
    {
        public string CategorySlug { get; set; }
    }
}