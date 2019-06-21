using System.Collections.Generic;
using MediatR;

namespace BlogAppCore.Application.Posts.Create
{
    public class CreatePostCommand : IRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> Tags { get; set; }

        public bool Published { get; set; }
    }
}