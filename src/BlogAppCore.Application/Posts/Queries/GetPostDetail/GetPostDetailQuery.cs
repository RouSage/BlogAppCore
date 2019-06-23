using BlogAppCore.Application.Posts.Models;
using MediatR;

namespace BlogAppCore.Application.Posts.Queries.GetPostDetail
{
    public class GetPostDetailQuery : IRequest<PostDetailDto>
    {
        public string Slug { get; set; }
    }
}