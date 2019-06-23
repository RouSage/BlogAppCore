using System.Collections.Generic;
using BlogAppCore.Application.Posts.Models;
using MediatR;

namespace BlogAppCore.Application.Posts.Queries.GetPostsPreview
{
    public class GetPostsPreviewQuery : IRequest<List<PostPreviewDto>>
    {

    }
}