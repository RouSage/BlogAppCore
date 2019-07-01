using System.Collections.Generic;
using BlogAppCore.Application.Posts.Models;
using MediatR;

namespace BlogAppCore.Application.Posts.Commands.Create
{
    public class CreatePostCommand : IRequest<PostDetailDto>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> Tags { get; set; }

        public bool Published { get; set; }
    }
}