using System.Collections.Generic;
using BlogAppCore.Application.Posts.Models;
using MediatR;

namespace BlogAppCore.Application.Posts.Queries.GetPostsByTag
{
    public class GetPostsByTagQuery : IRequest<List<PostPreviewDto>>
    {
        public string TagSlug { get; set; }
    }
}